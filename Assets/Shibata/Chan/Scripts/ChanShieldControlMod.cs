using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanShieldControlMod")]
public class ChanShieldControlMod : Module
{
	#region enum declare
	public enum State
	{
		ShotReady,
		Hold,
	}
	#endregion

	#region input
	ChanCameraControlMod _cameraControlMod;
	#endregion

	#region private fields
	[SerializeField]
	TFSound.AudioProperty _chargeAudioProp;
	[SerializeField]
	TFSound.AudioProperty _shotAudioProp;

	State _state;
	GameObject _holdingShield = null;
	ShieldCore _shieldCore;

	int _chargeSoundId;
	#endregion

	#region properties
	[SerializeField]
	GameObject _shieldPrefab = null;
	[SerializeField]
	float _genDistance = 1.0f;
	[SerializeField]
	float _genHeight = 1.0f;

	public State state { get { return _state; } }
	#endregion

	#region private methods
	void ShotReady()
	{

		if(Input.GetMouseButtonDown(0))
		{
			_holdingShield = Instantiate(_shieldPrefab);
			_holdingShield.transform.position = transform.position + transform.forward * _genDistance + transform.up * _genHeight;

			_shieldCore = _holdingShield.GetComponent<ShieldCore>();
			_shieldCore.destroySystem.isLock = true;
			_shieldCore.movementSystem.isLock = true;

			_state = State.Hold;

			_chargeSoundId = TFSound.Play(_chargeAudioProp);
		}
	}

	void Hold()
	{
		if (_holdingShield == null)
		{
			_state = State.ShotReady;
			return;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_shieldCore.destroySystem.isLock = false;
			_shieldCore.movementSystem.isLock = false;
			_holdingShield = null;
			_state = State.ShotReady;
			TFSound.Stop(_chargeSoundId);
			TFSound.Play(_shotAudioProp);
			return;
		}

		_holdingShield.transform.position = transform.position + transform.forward * _genDistance + transform.up * _genHeight;

		_holdingShield.transform.rotation = Quaternion.AngleAxis(_cameraControlMod.angleH, Vector3.up);
	}
	#endregion

	#region override methods
	protected override void ModuleAwake()
	{
		_cameraControlMod = GetComponent<ChanCameraControlMod>();
		_state = State.ShotReady;

		if (!_shieldPrefab)
			_shieldPrefab=(GameObject)Resources.Load("Shield");
	}

	public override void OrdableUpdate()
	{
		switch (_state)
		{
			case State.ShotReady:
				ShotReady();
				break;
			case State.Hold:
				Hold();
				break;
			default:
				break;
		}
	}

	#endregion
}
