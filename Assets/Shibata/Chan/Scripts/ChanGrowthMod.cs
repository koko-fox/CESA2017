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
	float _comboDiscardElapsed;
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
	/// <summary>コンボが中断されるまでの時間</summary>
	public float comboDiscardElapsed
	{
		get { return _comboDiscardElapsed; }
	}
	/// <summary>現在のコンボ数</summary>
	public int comboCount
	{
		get { return _comboCount; }
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
	#endregion

	#region event callbacks
	private void _provider_onDead(EnemyCore.DiedFactor factor)
	{
		if (factor == EnemyCore.DiedFactor.Suicided)
			return;

		_comboCount++;
		_comboDiscardElapsed = _comboDiscardDuration;
		_exp += _baseExp * _comboCount;
	}
	#endregion
}
