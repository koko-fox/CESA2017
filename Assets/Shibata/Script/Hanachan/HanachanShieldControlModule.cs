using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanShieldControlModule : MonoBehaviour
{
	[SerializeField]
	private float _shotCost;

	[SerializeField]
	private float _holdCost;

	[SerializeField]
	[Header("複製用シールドprefab")]
	private GameObject _shieldPrefab;

	[SerializeField]
	[Header("生成距離")]
	private float _generateDistance = 0.5f;

	[SerializeField]
	[Header("生成高さ")]
	private float _generateHeight = 1.0f;

	[SerializeField]
	private float _attackPower;

	public bool IsHold { get { return _inHoldShield; } }

	private GameObject _inHoldShield = null;
	private RadiateShieldController _inHoldShieldController;

	private delegate void ControlState();
	private ControlState _inState;

	private HanachanCore _core;
	private HanachanMovementModule _movementMod;
	private HanachanStatuses _statuses;
	private ShoulderCameraController _shoulderCam;

	private void InShotReady()
	{
		if (Input.GetKeyDown(KeyCode.F) && !_inHoldShield && _core.Energy >= _shotCost && !_movementMod.IsInDash)
		{
			_inHoldShield = Instantiate(_shieldPrefab);
			_inHoldShieldController = _inHoldShield.GetComponent<RadiateShieldController>();
			_inHoldShieldController.CurrentMode = RadiateShieldController.Mode.Retension;
			_inHoldShieldController.AttackPower = _attackPower;
			_inState = InHold;
		}
	}

	private void InHold()
	{
		_inHoldShield.transform.position = transform.position + transform.forward * _generateDistance + transform.up * _generateHeight;
		_inHoldShield.transform.rotation = Quaternion.AngleAxis(_shoulderCam.AngleH, Vector3.up);
		_core.Energy -= _holdCost * Time.deltaTime;

		if (Input.GetKeyUp(KeyCode.F))
		{
			_inHoldShieldController.CurrentMode = RadiateShieldController.Mode.Injection;
			_inHoldShield = null;
			_core.Energy -= _shotCost;
			_inState = InShotReady;
		}

		if (_holdCost * Time.deltaTime > _core.Energy)
		{
			Destroy(_inHoldShield);
			_inHoldShield = null;
			_inState = InShotReady;
		}
	}

	private void Awake()
	{
		_core = GetComponent<HanachanCore>();
		_movementMod = GetComponent<HanachanMovementModule>();
		_statuses = GetComponent<HanachanStatuses>();
		_shoulderCam = FindObjectOfType<ShoulderCameraController>();
	}

	private void Start()
	{
		_inState = InShotReady;
	}

	// Update is called once per frame
	void Update()
	{
		_inState();
	}
}
