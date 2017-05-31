using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ChanGrowthSystem : Lockable
{
	DebugPanel _debugPanel;

	ChanCore _core;
	[SerializeField]
	EnemyProvider _provider;

	[SerializeField]
	int _maxLevel = 100;
	int _level = 0;
	/// <summary>
	/// プレイヤーレベル
	/// </summary>
	public int level { get { return _level; } }

	[SerializeField]
	AnimationCurve _expCurve;
	[SerializeField]
	AnimationCurve _healthCurve;

	public delegate void OnValueChanged();

	/// <summary>
	/// レベル変更イベント
	/// </summary>
	public event OnValueChanged onLevelChanged = delegate { };
	/// <summary>
	/// 経験値変更イベント
	/// </summary>
	public event OnValueChanged onExpChanged = delegate { };

	int GetLevel(float expValue)
	{
		int length = _expCurve.length;
		for (int f1 = 0; f1 < length; f1++)
		{
			if (expValue < _expCurve.keys[f1].value)
				return Mathf.Clamp(f1 - 1, 0, _maxLevel);
		}
		return -1;
	}

	float _exp;
	/// <summary>
	/// 経験値
	/// </summary>
	public float exp
	{
		get { return _exp; }
		set
		{
			_exp = value;
			int next = GetLevel(_exp);
			if (_level != next && next != -1)
			{
				_core.healthSystem.maxHealth = _healthCurve.keys[next].value;
				_core.healthSystem.health = _core.healthSystem.maxHealth;

				onLevelChanged();
			}
			_level = next;
			onExpChanged();
		}
	}

	/// <summary>
	/// 次レベルの要求EXP
	/// </summary>
	public float nextRequireExp
	{
		get
		{
			int index = Mathf.Clamp(_level + 1, 0, _maxLevel);
			return _expCurve.keys[index].value;
		}
	}
	
	/// <summary>
	/// 前レベルの要求EXP
	/// </summary>
	public float prevRequireExp
	{
		get
		{
			return _expCurve.keys[_level].value;
		}
	}

	private void Reset()
	{
		for (int f1 = 0; f1 <= _maxLevel; f1++)
		{
			_expCurve.AddKey(f1, ((f1 + 1) * 100.0f));
			_healthCurve.AddKey(f1, ((f1 + 1) * 100.0f));
		}
	}

	private void Awake()
	{
		_core = GetComponent<ChanCore>();

		if (!_provider)
			_provider = FindObjectOfType<EnemyProvider>();

		_debugPanel = DebugPanelManager.instance.Create(gameObject);
		_debugPanel.fontSize = 7;
		_debugPanel.offset = new Vector3(0.0f, 1.4f, -0.5f);
	}

	private void Start()
	{
		if (!_provider)
			return;

		_provider.onDead += _provider_onDead;
	}

	private void _provider_onDead()
	{
		exp += 100.0f;
	}

	private void FixedUpdate()
	{
		_debugPanel.text = "";
		StringBuilder buf = new StringBuilder();

		buf.Append("EXP:").Append(_exp).Append("\n");
		buf.Append("Lv:").Append(GetLevel(_exp)).Append("\n");
		for(int f1=1;f1<10;f1++)
		{
			buf.Append("exp require Lv").Append(f1).Append(":").Append(_expCurve[f1].value).Append("\n");
		}

		_debugPanel.text = buf.ToString();
	}
}
