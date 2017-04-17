using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HanachanController
{
	[SerializeField]
	[Header("複製するシールドのprefab")]
	private GameObject _radiateShieldPrefab;

	private GameObject _holdingShield = null;

	private delegate void ShieldControlState();
	private ShieldControlState _shieldControlState;

	private void ShieldControl()
	{
	}

}
