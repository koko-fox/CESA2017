using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorOrbController : OrbBase
{
	[SerializeField]
	[Tooltip("増加量")]
	private float amount = 1;

	protected override void OnCollisionTarget()
	{
		base.OnCollisionTarget();

		//_chanCore.Statuses.ArmorValue += amount;
	}
}
