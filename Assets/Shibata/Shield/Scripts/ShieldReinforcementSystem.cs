using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldReinforcementSystem : Lockable
{
	ShieldCore _core;

	[SerializeField]
	float _maxScale = 3.0f;
	[SerializeField]
	float _minScale = 1.0f;
	[SerializeField]
	float _expansionSpeed = 1.0f;

	private void Awake()
	{
		_core = GetComponent<ShieldCore>();
		transform.localScale = new Vector3(_minScale, transform.localScale.y, transform.localScale.z);
	}

	private void Start()
	{
	}

	protected override void LockableUpdate()
	{
		if (_core.destroySystem.isLock)
		{
			if (_maxScale >= transform.localScale.x)
			{
				Vector3 scale = transform.localScale;
				scale.x += _expansionSpeed * Time.deltaTime;
				transform.localScale = scale;
			}
			else
			{
				transform.localScale = new Vector3(_maxScale, transform.localScale.y, transform.localScale.z);
			}
		}
	}
}
