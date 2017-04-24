using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadShieldMovementSystem : MonoBehaviour
{
	[SerializeField]
	private float _force;

	private Rigidbody _body;
	private RadShieldDestroySystem _destroySystem;

	private bool _MoveEnabled = true;
	public bool MoveEnabled
	{
		get { return _MoveEnabled; }
		set
		{
			_MoveEnabled = value;
			_destroySystem.enabled = value;
		}
	}

	private void Awake()
	{
		_body = GetComponent<Rigidbody>();
		_destroySystem = GetComponent<RadShieldDestroySystem>();
	}

	private void FixedUpdate()
	{
		if(_MoveEnabled)
			_body.AddForce(transform.forward * _force, ForceMode.VelocityChange);
	}
}
