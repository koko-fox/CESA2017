using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Specter/SpecterMovementSystem")]
public class SpecterMovementSystem : Lockable
{
	SpecterCore _core;

	[SerializeField]
	float _speed = 10.0f;

	[SerializeField]
	KeyCode _forwardKey = KeyCode.W;
	[SerializeField]
	KeyCode _leftKey = KeyCode.A;
	[SerializeField]
	KeyCode _rightKey = KeyCode.D;
	[SerializeField]
	KeyCode _backKey = KeyCode.S;
	[SerializeField]
	KeyCode _upKey = KeyCode.Space;
	[SerializeField]
	KeyCode _downKey = KeyCode.LeftControl;

	private void Awake()
	{
		_core = GetComponent<SpecterCore>();
	}

	protected override void LockableUpdate()
	{
		bool isForward = Input.GetKey(_forwardKey);
		bool isLeft = Input.GetKey(_leftKey);
		bool isRight = Input.GetKey(_rightKey);
		bool isBack = Input.GetKey(_backKey);
		bool isUp = Input.GetKey(_upKey);
		bool isDown = Input.GetKey(_downKey);

		Vector3 vel = Vector3.zero;

		if (isForward && !isBack)
			vel.z += _speed;
		if (!isForward && isBack)
			vel.z -= _speed;
		if (isLeft && !isRight)
			vel.x -= _speed;
		if (!isLeft && isRight)
			vel.x += _speed;

		vel = transform.TransformDirection(vel);
		if (isUp && !isDown)
			vel.y += _speed;
		if (!isUp && isDown)
			vel.y -= _speed;

		transform.position += vel * Time.deltaTime;
	}
}
