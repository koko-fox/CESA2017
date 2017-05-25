using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbController : OrbBase
{
	[SerializeField]
	[Tooltip("回復率(%)")]
	[Range(0.0f, 100.0f)]
	private float recoveryRate;

	protected override void OnCollisionTarget()
	{
		//_hanachanCore.Health += _hanachanCore.MaxHealth * (recoveryRate / 100.0f);
	}
}
