using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanMovementModule : MonoBehaviour
{
	[SerializeField]
	[Header("アニメーション用のモデル")]
	private GameObject _modelForAnimation;

	[SerializeField]
	private float _runSpeed;

	/// <summary>
	/// Hanachanステータス定義モジュール
	/// </summary>
	private HanachanStatuses _statuses;

	/// <summary>
	/// アニメーター
	/// </summary>
	private Animator _animator;

	/// <summary>
	/// 肩越しカメラ
	/// </summary>
	private ShoulderCameraController _shoulderCam;

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_animator = _modelForAnimation.GetComponent<Animator>();
		_shoulderCam = FindObjectOfType<ShoulderCameraController>();
		_statuses = GetComponent<HanachanStatuses>();
		_rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 velocity = Vector3.zero;
		bool isForward = Input.GetKey(KeyCode.W);
		bool isLeft = Input.GetKey(KeyCode.A);
		bool isRight = Input.GetKey(KeyCode.D);
		bool isBack = Input.GetKey(KeyCode.S);
		bool isDash = Input.GetKey(KeyCode.LeftShift);

		if (isForward && !isBack)
			velocity.z += _statuses.ForwardSpeed;
		if (isLeft && !isRight)
			velocity.x += -_statuses.SideWalkSpeed;
		if (isRight && !isLeft)
			velocity.x += _statuses.SideWalkSpeed;
		if (isBack && !isForward)
			velocity.z += -_statuses.BackSpeed;

		if (isDash)
			velocity *= _statuses.SpeedMagByDash;

		if (isForward || isLeft || isRight || isBack)
			_animator.SetBool("Forward", true);
		else
			_animator.SetBool("Forward", false);

		if (isDash)
			_animator.SetBool("Dash", true);
		else
			_animator.SetBool("Dash", false);

		velocity = transform.TransformDirection(velocity);
		//transform.position += velocity * Time.deltaTime;
		_rigidbody.velocity = velocity;
		transform.rotation = Quaternion.AngleAxis(_shoulderCam.AngleH, Vector3.up);
	}
}
