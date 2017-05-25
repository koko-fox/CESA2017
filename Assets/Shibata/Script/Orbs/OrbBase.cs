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
	protected ChanCore _chanCore;

	protected Rigidbody _rigidBody;
	protected SphereCollider _sphereCollider;

	/// <summary>
	/// ターゲットに接触したときの処理
	/// </summary>
	protected virtual void OnCollisionTarget() { }
	
	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
		_sphereCollider = GetComponent<SphereCollider>();
		_chanCore = FindObjectOfType<ChanCore>();
		_target = _chanCore.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float distance = Vector3.Distance(transform.position+new Vector3(0.0f,0.7f,0.0f), _target.transform.position);
		if(distance<_vacuumRadius)
		{
			Vector3 dir = (_target.transform.position - transform.position).normalized;
			_rigidBody.AddForce(_vacuumForce * dir, ForceMode.VelocityChange);
		}

		var hits = Physics.SphereCastAll(transform.position, _sphereCollider.radius, transform.forward, 0.1f);

		foreach (var hit in hits)
		{
			if (hit.transform.gameObject == _target)
			{
				OnCollisionTarget();
				Destroy(transform.gameObject);
				break;
			}
		}
	}
}
