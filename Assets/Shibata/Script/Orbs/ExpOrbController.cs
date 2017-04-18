using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpOrbController : OrbBase
{
	[SerializeField]
	[Tooltip("増加量")]
	private float amount;

	protected override void OnCollisionTarget()
	{
		base.OnCollisionTarget();
		_hanachanCore.Statuses.ExpOrbNum += amount;
	}
}
