using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class ChanFacadeHolder : MonoBehaviour
{
	ChanFacade _facade;
	public ChanFacade facade
	{
		get { return _facade; }
	}

	private void Awake()
	{
		_facade = new ChanFacade(gameObject);
	}
}

public class ChanFacade
{
	#region inputs
	ChanHealthMod _healthMod;
	ChanMovementMod _movementMod;
	ChanCameraControlMod _cameraControlMod;
	ChanShieldControlMod _shieldControlMod;
	ChanGrowthMod _growthMod;
	ChanBurstMod _burstMod;
	#endregion
	/*
	#region health modules
	/// <summary>ヘルス</summary>
	public float health
	{
		get { return _healthMod.health; }
		set { _healthMod.health = value; }
	}

	/// <summary>最大ヘルス</summary>
	public float maxHealth
	{
		get { return _healthMod.maxHealth; }
		set { _healthMod.maxHealth = value; }
	}

	/// <summary>ヘルス回復速度</summary>
	public float regenSpeed
	{
		get { return _healthMod.regenSpeed; }
		set { _healthMod.regenSpeed = value; }
	}
	#endregion

	#region movement modules
	public List<ChanMovementMod.SpeedMultiplier> moveSpeedMultiplier
	{
		get { return _movementMod.speedMultipliers; }
	}
	#endregion

	#region shield control modules
	/// <summary>シールド操作の状態</summary>
	public ChanShieldControlMod.State shieldControlState { get { return _shieldControlMod.state; } }
	#endregion

	#region growth modules
	public int level { get { return _growthMod.level; } }
	public float exp
	{
		get { return _growthMod.exp; }
		set { _growthMod.exp = value; }
	}
	public float requireExpForLevelUp { get { return _growthMod.requireExpForLevelUp; } }
	public float prevRequireExp { get { return _growthMod.prevRequireExp; } }
	public float comboDiscardElapsed { get { return _growthMod.comboDiscardElapsed; } }
	public float comboCount { get { return _growthMod.comboCount; } }
	#endregion
	*/

	#region properties
	public ChanHealthMod healthMod { get { return _healthMod; } }
	public ChanMovementMod movementMod { get { return _movementMod; } }
	public ChanCameraControlMod cameraControlMod { get { return _cameraControlMod; } }
	public ChanShieldControlMod shieldControlMod { get { return _shieldControlMod; } }
	public ChanGrowthMod growthMod { get { return _growthMod; } }
	public ChanBurstMod burstMod { get { return _burstMod; } }
	#endregion

	public ChanFacade(GameObject obj)
	{
		_healthMod = obj.GetComponent<ChanHealthMod>();
		_cameraControlMod = obj.GetComponent<ChanCameraControlMod>();
		_movementMod = obj.GetComponent<ChanMovementMod>();
		_shieldControlMod = obj.GetComponent<ChanShieldControlMod>();
		_growthMod = obj.GetComponent<ChanGrowthMod>();
		_burstMod = obj.GetComponent<ChanBurstMod>();
	}
}
