using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Specter/SpecterCore")]
public class SpecterCore : Lockable
{
	List<Lockable> _systems = new List<Lockable>();

	SpecterMovementSystem _movementSystem;
	/// <summary>
	/// 移動システムへのアクセス
	/// </summary>
	public SpecterMovementSystem movementSystem { get { return _movementSystem; } }

	SpecterCameraControlSystem _cameraControlSystem;
	/// <summary>
	/// カメラ操作システムへのアクセス
	/// </summary>
	public SpecterCameraControlSystem cameraControlSystem { get { return _cameraControlSystem; } }

	SpecterConstructorSystem _constructorSystem;
	/// <summary>
	/// オブジェクト生成システムへのアクセス
	/// </summary>
	public SpecterConstructorSystem constructorSystem { get { return _constructorSystem; } }

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

	private void Awake()
	{
		_movementSystem = GetComponent<SpecterMovementSystem>();
		_cameraControlSystem = GetComponent<SpecterCameraControlSystem>();
		_constructorSystem = GetComponent<SpecterConstructorSystem>();

		_systems.Add(_movementSystem);
		_systems.Add(_cameraControlSystem);
		_systems.Add(_constructorSystem);
	}
}
