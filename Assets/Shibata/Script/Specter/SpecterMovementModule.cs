using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterMovementModule : MonoBehaviour
{
	[SerializeField]
	[Header("移動速度")]
	private float _moveSpeed;
	
	// Update is called once per frame
	void Update ()
	{
		bool isForward = Input.GetKey(KeyCode.W);
		bool isBack = Input.GetKey(KeyCode.S);
		bool isLeft = Input.GetKey(KeyCode.A);
		bool isRight = Input.GetKey(KeyCode.D);

		Vector3 mover = Vector3.zero;

		if (isForward && !isBack)
			mover.z += _moveSpeed * Time.deltaTime;
		if (!isForward && isBack)
			mover.z -= _moveSpeed * Time.deltaTime;
		if (isLeft && !isRight)
			mover.x -= _moveSpeed * Time.deltaTime;
		if (!isLeft && isRight)
			mover.x += _moveSpeed * Time.deltaTime;

		mover = transform.TransformDirection(mover);
		transform.position += mover;
	}
}
