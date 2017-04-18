using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanShieldControlModule : MonoBehaviour
{
	[SerializeField]
	[Header("複製用シールドprefab")]
	private GameObject _shieldPrefab;

	[SerializeField]
	[Header("生成距離")]
	private float _generateDistance = 0.5f;

	[SerializeField]
	[Header("生成高さ")]
	private float _generateHeight = 1.0f;

	private GameObject _inHoldShield = null;
	private RadiateShieldController _inHoldShieldController;

	private delegate void ControlState();
	private ControlState _inState;

	private HanachanStatuses _statuses;
	private ShoulderCameraController _shoulderCam;

	private void InShotReady()
	{
		if (Input.GetKeyDown(KeyCode.F) && !_inHoldShield && _statuses.EnergyValue >= _statuses.ShieldShotCost)
		{
			_inHoldShield = Instantiate(_shieldPrefab);
			_inHoldShieldController = _inHoldShield.GetComponent<RadiateShieldController>();
			_inHoldShieldController.CurrentMode = RadiateShieldController.Mode.Retension;
			_inState = InHold;
		}
	}

	private void InHold()
	{
		_inHoldShield.transform.position = transform.position + transform.forward * _generateDistance + transform.up * _generateHeight;
		_inHoldShield.transform.rotation = Quaternion.AngleAxis(_shoulderCam.AngleH, Vector3.up);
		_statuses.EnergyValue -= _statuses.ShieldHoldCost * Time.deltaTime;

		if (Input.GetKeyUp(KeyCode.F))
		{
			_inHoldShieldController.CurrentMode = RadiateShieldController.Mode.Injection;
			_inHoldShield = null;
			_statuses.EnergyValue -= _statuses.ShieldShotCost;
			_inState = InShotReady;
		}

		if (_statuses.ShieldHoldCost * Time.deltaTime > _statuses.EnergyValue)
		{
			Destroy(_inHoldShield);
			_inHoldShield = null;
			_inState = InShotReady;
		}
	}

	private void Awake()
	{
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
