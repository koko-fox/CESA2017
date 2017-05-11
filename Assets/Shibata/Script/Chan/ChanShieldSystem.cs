using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanShieldSystem")]
public class ChanShieldSystem : Lockable
{
	ChanCore _core;

	[SerializeField]
	float _shotCost;
	[SerializeField]
	float _holdCost;

	[SerializeField]
	GameObject _shieldPrefab;

	[SerializeField]
	float _distance = 1.0f;
	float _height = 1.0f;

	bool _isHolding = false;
	/// <summary>
	/// シールドをホールドしているか
	/// </summary>
	public bool isHolding { get { return _isHolding; } }

	GameObject _holdedShield = null;
	ShieldCore _shieldCore;

	delegate void State();
	State _state;

	void ShotReady()
	{
		if(Input.GetMouseButtonDown(0)&&_core.energySystem.energy>=_shotCost)
		{
			_holdedShield = Instantiate(_shieldPrefab);
			_holdedShield.transform.position = transform.position + transform.forward * _distance + transform.up * _height;

			_shieldCore = _holdedShield.GetComponent<ShieldCore>();
			_shieldCore.destroySystem.isLock = true;

			_isHolding = true;
			_core.energySystem.energy -= _shotCost;
			_state = Hold;

			_core.movementSystem.dashLock = true;
			_core.energySystem.isLock = true;
		}
	}

	void Hold()
	{
		_holdedShield.transform.position = transform.position + transform.forward * _distance + transform.up * _height;

		_holdedShield.transform.rotation = Quaternion.AngleAxis(_core.cameraControlSystem.angleH, Vector3.up);
		_core.energySystem.energy -= _holdCost * Time.deltaTime;

		if(Input.GetMouseButtonUp(0))
		{
			_shieldCore.destroySystem.isLock = false;
			_holdedShield = null;
			_isHolding = false;
			_state = ShotReady;

			_core.movementSystem.dashLock = false;
			_core.energySystem.isLock = false;
		}

		if(_holdCost*Time.deltaTime>_core.energySystem.energy)
		{
			Destroy(_holdedShield);
			_holdedShield = null;
			_state = ShotReady;

			_core.movementSystem.dashLock = false;
			_core.energySystem.isLock = false;
		}
	}

	void Awake()
	{
		_core = GetComponent<ChanCore>();
		_state = ShotReady;
	}

	protected override void LockableUpdate()
	{
		_state();
	}

}
