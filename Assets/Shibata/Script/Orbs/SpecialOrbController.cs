using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOrbController : OrbBase
{
	[SerializeField]
	[Tooltip("回復率")]
	[Range(0,100)]
	private float recoveryRate;

	protected override void OnCollisionTarget()
	{
		base.OnCollisionTarget();
		_hanachanCore.Statuses.SpecialValue += recoveryRate/100.0f * _hanachanCore.Statuses.MaxSpecial;
	}
}
