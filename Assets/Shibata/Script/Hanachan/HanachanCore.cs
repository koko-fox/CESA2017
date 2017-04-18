using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanCore : MonoBehaviour
{
	private HanachanMovementModule _movementModule;
	public HanachanMovementModule MovementModule { get { return _movementModule; } }

	private HanachanShieldControlModule _shieldControlModule;
	public HanachanShieldControlModule ShieldControlModule { get { return _shieldControlModule; } }

	private HanachanStatuses _statuses;
	public HanachanStatuses Statuses { get { return _statuses; } }

	private void Awake()
	{
		_movementModule = GetComponent<HanachanMovementModule>();
		_shieldControlModule = GetComponent<HanachanShieldControlModule>();
		_statuses = GetComponent<HanachanStatuses>();
	}
}
