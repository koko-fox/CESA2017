using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanEnergySystem")]
/// <summary>
/// エネルギーの概念を追加するモジュール
/// </summary>
public class ChanEnergySystem : Lockable
{
	public delegate void OnValueChanged();
	public event OnValueChanged onEnergyChanged = delegate { };

	bool _isZero;
	/// <summary>
	/// エネルギーがゼロ以下か
	/// </summary>
	public bool isZero { get { return _isZero; } }

	[SerializeField]
	float _maxEnergy;
	/// <summary>
	/// 最大エネルギー
	/// </summary>
	public float maxEnergy
	{
		get { return _maxEnergy; }
		set { _maxEnergy = Mathf.Clamp(value,0.0f,value); }
	}

	float _energy;
	/// <summary>
	/// エネルギーの現在値
	/// </summary>
	public float energy
	{
		get { return _energy; }
		set
		{
			_isZero = (_energy - value) <= 0.0f;
			_energy = Mathf.Clamp(value, 0.0f, maxEnergy);
			onEnergyChanged();
		}
	}

	[SerializeField]
	float _regenSpeed;
	/// <summary>
	/// 回復速度
	/// </summary>
	public float regenSpeed
	{
		get { return _regenSpeed; }
		set { _regenSpeed = value; }
	}

	private void Awake()
	{
		energy = maxEnergy;
	}

	protected override void LockableUpdate()
	{
		energy += regenSpeed * Time.deltaTime;
	}
}
