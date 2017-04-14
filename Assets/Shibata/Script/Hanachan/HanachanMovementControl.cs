using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HanachanController
{
	/// <summary>
	/// アニメーター
	/// </summary>
	private Animator _animator;

	/// <summary>
	/// 肩越しカメラ
	/// </summary>
	private ShoulderCameraController _shoulderCam;

	private void MovementControl()
	{
		Vector3 velocity = Vector3.zero;
		bool isForward = Input.GetKey(KeyCode.W);
		bool isLeft = Input.GetKey(KeyCode.A);
		bool isRight = Input.GetKey(KeyCode.D);
		bool isBack = Input.GetKey(KeyCode.S);

		if (isForward && !isBack)
			velocity.z += _forwardSpeed;
		if (isLeft && !isRight)
			velocity.x += -_sideWalkSpeed;
		if (isRight && !isLeft)
			velocity.x += _sideWalkSpeed;
		if (isBack && !isForward)
			velocity.z += -_backSpeed;

		if (isForward || isLeft || isRight || isBack)
			_animator.SetBool("Forward", true);
		else
			_animator.SetBool("Forward", false);

		velocity = transform.TransformDirection(velocity);
		transform.position += velocity * Time.deltaTime;

		transform.rotation = Quaternion.AngleAxis(_shoulderCam.AngleH, Vector3.up);
	}
}
