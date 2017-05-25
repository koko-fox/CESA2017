using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanGrowthSystem : Lockable
{
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
	[SerializeField]
	AnimationCurve _energyCurve;

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
			int next = GetLevel(_exp);
			if (_level != next && next != -1)
			{
				_core.healthSystem.maxHealth = _healthCurve.keys[next].value;
				_core.energySystem.maxEnergy = _energyCurve.keys[next].value;

				_core.healthSystem.health = _core.healthSystem.maxHealth;
				_core.energySystem.energy = _core.energySystem.maxEnergy;

				onLevelChanged();
			}
			_level = next;

			_exp = value;
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
			_expCurve.AddKey(f1, (f1 * f1)+1);
			_healthCurve.AddKey(f1, (f1 * f1) + 1);
			_energyCurve.AddKey(f1, (f1 * f1) + 1);
		}
	}

	private void Awake()
	{
		_core = GetComponent<ChanCore>();

		if (!_provider)
			_provider = FindObjectOfType<EnemyProvider>();
	}

	private void Start()
	{
		if (!_provider)
			return;

		_provider.onDead += _provider_onDead;
	}

	private void _provider_onDead()
	{
		_exp += 1.0f;
	}

	private void Update()
	{
	}
}
