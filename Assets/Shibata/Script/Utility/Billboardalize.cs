using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboardalize : MonoBehaviour
{
	[SerializeField]
	Camera _target;

	[SerializeField]
	bool _flipY = false;

	private void Awake()
	{
		if(!_target)
			_target = FindObjectOfType<Camera>();
	}

	private void Update()
	{
		transform.LookAt(_target.transform);
		if(_flipY)
			transform.Rotate(Vector3.up, 180);
	}
}
