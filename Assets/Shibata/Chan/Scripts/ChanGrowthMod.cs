using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanGrowthMod")]
public class ChanGrowthMod : Module
{
	#region events
	public delegate void ValueChangeEventHandler();
	/// <summary>レベル変更イベント</summary>
	public event ValueChangeEventHandler onLevelChanged = delegate { };
	/// <summary>exp変更イベント</summary>
	public event ValueChangeEventHandler onExpChanged = delegate { };
	/// <summary>コンボ開始イベント</summary>
	public event ValueChangeEventHandler onStartCombo = delegate { };
	/// <summary>コンボ中イベント</summary>
	public event ValueChangeEventHandler onWhileCombo = delegate { };
	/// <summary>コンボ終了イベント</summary>
	public event ValueChangeEventHandler onEndCombo = delegate { };
	#endregion

	#region inputs
	ChanHealthMod _healthMod;
	[SerializeField]
	EnemyProvider _provider;
	#endregion

	#region private fields
	[SerializeField]
	int _maxLevel = 100;
	int _level = 0;
	float _exp = 0;
	[SerializeField]
	float _baseExp;
	int _comboCount;
	[SerializeField]
	float _comboDiscardDuration;
	float _comboRemainTime;
	[SerializeField]
	AnimationCurve _expCurve;
	[SerializeField]
	AnimationCurve _healthCurve;
	#endregion

	#region properties
	/// <summary>プレイヤーレベル</summary>
	public int level { get { return _level; } }
	/// <summary>経験値</summary>
	public float exp
	{
		get { return _exp; }
		set
		{
			_exp = value;
			onExpChanged();
			int nextLevel = GetLevel(_exp);
			if(_level!=nextLevel&&nextLevel!=-1)
			{
				_healthMod.maxHealth = _healthCurve.keys[nextLevel].value;
				_healthMod.health = _healthMod.maxHealth;
				_level = nextLevel;
				onLevelChanged();
			}
		}
	}
	/// <summary>レベルアップに必要な経験値</summary>
	public float requireExpForLevelUp
	{
		get
		{
			int index = Mathf.Clamp(_level + 1, 0, _maxLevel);
			return _expCurve.keys[index].value;
		}
	}
	/// <summary>前レベルの要求EXP</summary>
	public float prevRequireExp
	{
		get { return _expCurve.keys[_level].value; }
	}
	/// <summary>コンボ持続時間</summary>
	public float comboDiscardDuration { get { return _comboDiscardDuration; } }
	/// <summary>コンボが中断されるまでの時間</summary>
	public float comboRemainTime
	{
		get { return _comboRemainTime; }
	}
	/// <summary>現在のコンボ数</summary>
	public int comboCount
	{
		get { return _comboCount; }
	}
	/// <summary>コンボによる経験値倍率</summary>
	public float expMultiplierFromCombo
	{
		get{ return 1f + Mathf.Clamp(_comboCount * 0.01f, 0f, 1f); }
	}
	#endregion

	#region private methods
	/// <summary>経験値量からレベルを算出する</summary>
	/// <param name="expValue">経験値量</param>
	/// <returns>算出されたレベル</returns>
	int GetLevel(float expValue)
	{
		int length = _expCurve.length;
		for(int f1=0;f1<length;f1++)
		{
			if (expValue < _expCurve[f1].value)
				return Mathf.Clamp(f1 - 1, 0, _maxLevel);
		}
		return -1;
	}
	#endregion

	#region override/unity methods
	private void Reset()
	{
		for(int f1=0;f1<=_maxLevel;f1++)
		{
			_expCurve.AddKey(f1, ((f1 + 1) * 100.0f));
			_healthCurve.AddKey(f1,((f1 + 1) * 100.0f));
		}
	}

	protected override void ModuleAwake()
	{
		_healthMod = GetComponent<ChanHealthMod>();
		if (!_provider)
			_provider = FindObjectOfType<EnemyProvider>();
	}

	public override void OrdableStart()
	{
		if (!_provider)
			return;

		_provider.onDead += _provider_onDead;
	}

	public override void OrdableUpdate()
	{
		if(_comboRemainTime>=0f)
		{
			_comboRemainTime -= Time.deltaTime;
			onWhileCombo();
			if (_comboRemainTime < 0f)
			{
				_comboCount = 0;
				onEndCombo();
			}
		}

		if (Input.GetKeyDown(KeyCode.P))
			_provider.ThrowDummy();
	}
	#endregion

	#region event callbacks
	private void _provider_onDead(EnemyCore.DeathInfo info)
	{
		if (info.Factor == EnemyCore.DeathInfo.DeathFactor.Suicided) 
			return;

		_comboCount++;
		_comboRemainTime = _comboDiscardDuration;
		_exp += _baseExp * expMultiplierFromCombo;

		if (_comboCount == 1)
			onStartCombo();
	}
	#endregion
}
