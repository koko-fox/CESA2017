using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Shield/ShieldDestroySystem")]
public class ShieldDestroySystem : Lockable
{
	[SerializeField]
	float _lifeTime = 3.0f;

	float _elapsedTime = 0.0f;

	protected override void LockableUpdate()
	{
		_elapsedTime += Time.deltaTime;

		if (_elapsedTime >= _lifeTime)
			Destroy(gameObject);
	}
}
