﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBase : MonoBehaviour
{
	[SerializeField]
	[Tooltip("吸引範囲")]
	protected float _vacuumRadius;

	[SerializeField]
	[Tooltip("吸引力")]
	protected float _vacuumForce;

	//対象
	protected GameObject _target;
	protected UnityChanController _unityChanController;

	protected Rigidbody _rigidBody;
	protected SphereCollider _sphereCollider;

	/// <summary>
	/// ターゲットに接触したときの処理
	/// </summary>
	protected virtual void OnCollisionTarget() { }

	// Use this for initialization
	void Start ()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_sphereCollider = GetComponent<SphereCollider>();
		_unityChanController = FindObjectOfType<UnityChanController>();
		_target = _unityChanController.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float distance = Vector3.Distance(transform.position, _target.transform.position);
		if(distance<_vacuumRadius)
		{
			Vector3 dir = (_target.transform.position - transform.position).normalized;
			_rigidBody.AddForce(_vacuumForce * dir, ForceMode.VelocityChange);
		}

		var hits = Physics.SphereCastAll(transform.position, _sphereCollider.radius, transform.forward);

		foreach(var hit in hits)
		{
			if(hit.transform.gameObject==_target)
			{
				OnCollisionTarget();
				Destroy(transform.gameObject);
				break;
			}
		}
	}
}