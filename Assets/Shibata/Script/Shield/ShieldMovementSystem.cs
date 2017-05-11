using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovementSystem : Lockable
{
	[SerializeField]
	float _force;

	Rigidbody _body;

	private void Awake()
	{
		_body = GetComponent<Rigidbody>();
	}

	protected override void LockableFixedUpdate()
	{
		Vector3 vel = Vector3.zero;

		vel.z += _force;
		vel = transform.TransformDirection(vel);

		_body.AddForce(vel, ForceMode.VelocityChange);
	}
}
