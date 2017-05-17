﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanCore")]
public class ChanCore : MonoBehaviour
{
	public struct LockInfo
	{
		public bool locked;
		public string name;
	}

	List<Lockable> _systems = new List<Lockable>();

	ChanHealthSystem _healthSystem;
	/// <summary>
	/// ヘルスシステムへのアクセス
	/// </summary>
	public ChanHealthSystem healthSystem { get { return _healthSystem; } }

	ChanEnergySystem _energySystem;
	/// <summary>
	/// エネルギーシステムへのアクセス
	/// </summary>
	public ChanEnergySystem energySystem { get { return _energySystem; } }

	ChanMovementSystem _movementSystem;
	/// <summary>
	/// 移動システムへのアクセス
	/// </summary>
	public ChanMovementSystem movementSystem { get { return _movementSystem; } }

	ChanShieldSystem _shieldSystem;
	/// <summary>
	/// シールドシステムへのアクセス
	/// </summary>
	public ChanShieldSystem shieldSystem { get { return _shieldSystem; } }

	ChanCameraControlSystem _cameraControlSystem;
	/// <summary>
	/// カメラ操作システムへのアクセス
	/// </summary>
	public ChanCameraControlSystem cameraControlSystem { get { return _cameraControlSystem; } }

	ChanGrowthSystem _growthSystem;
	/// <summary>
	/// 成長システムへのアクセス
	/// </summary>
	public ChanGrowthSystem growthSystem { get { return _growthSystem; } }

	/// <summary>
	/// 全てのシステムをロックする
	/// </summary>
	public void LockAll()
	{
		foreach (var elem in _systems)
			elem.isLock = true;
	}

	/// <summary>
	/// 全てのシステムをアンロックする
	/// </summary>
	public void UnlockAll()
	{
		foreach (var elem in _systems)
			elem.isLock = false;
	}

	/// <summary>
	/// 全てのシステムのロック状況を取得する
	/// </summary>
	/// <returns></returns>
	public List<LockInfo> GetLockInfo()
	{
		var info = new List<LockInfo>();

		foreach(var elem in _systems)
		{
			LockInfo buf;
			buf.locked = elem.isLock;
			buf.name = elem.GetType().Name;
			info.Add(buf);
		}

		return info;
	}

	private void Awake()
	{
		_healthSystem = GetComponent<ChanHealthSystem>();
		_energySystem = GetComponent<ChanEnergySystem>();
		_movementSystem = GetComponent<ChanMovementSystem>();
		_shieldSystem = GetComponent<ChanShieldSystem>();
		_cameraControlSystem = GetComponent<ChanCameraControlSystem>();
		_growthSystem = GetComponent<ChanGrowthSystem>();

		_systems.Add(_healthSystem);
		_systems.Add(_energySystem);
		_systems.Add(_movementSystem);
		_systems.Add(_shieldSystem);
		_systems.Add(_cameraControlSystem);
		_systems.Add(_growthSystem);
	}
}
