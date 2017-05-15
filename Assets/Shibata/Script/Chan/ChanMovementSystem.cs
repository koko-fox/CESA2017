using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[AddComponentMenu("Chan/ChanMovementSystem")]
/// <summary>
/// 移動機能を追加するモジュール
/// </summary>
public class ChanMovementSystem : Lockable
{
	ChanCore _core;
	Rigidbody _body;

	[SerializeField]
	GameObject _model;
	Animator _animator;

	[SerializeField]
	float _baseSpeed = 3.0f;
	[SerializeField]
	float _sideWalkMul = 0.75f;
	[SerializeField]
	float _backWalkMul = 0.5f;
	[SerializeField]
	float _dashMul = 1.5f;

	[SerializeField]
	KeyCode _forwardKey = KeyCode.W;
	[SerializeField]
	KeyCode _leftKey = KeyCode.A;
	[SerializeField]
	KeyCode _rightKey = KeyCode.D;
	[SerializeField]
	KeyCode _backKey = KeyCode.S;
	[SerializeField]
	KeyCode _dashKey = KeyCode.LeftShift;

	bool _dashLock = false;
	/// <summary>
	/// ダッシュ機能のロックフラグ
	/// </summary>
	public bool dashLock
	{
		get { return _dashLock; }
		set { _dashLock = value; }
	}

	private void Awake()
	{
		_core = GetComponent<ChanCore>();
		_body = GetComponent<Rigidbody>();
		_animator = _model.GetComponent<Animator>();
	}

	protected override void LockableFixedUpdate()
	{
		Vector3 velocity = Vector3.zero;
		bool isForward = Input.GetKey(_forwardKey);
		bool isLeft = Input.GetKey(_leftKey);
		bool isRight = Input.GetKey(_rightKey);
		bool isBack = Input.GetKey(_backKey);
		bool isDash = Input.GetKey(_dashKey) && !dashLock;

		if (isForward && !isBack)
			velocity.z += _baseSpeed;
		if (isLeft && !isRight)
			velocity.x -= _baseSpeed * _sideWalkMul;
		if (isRight && !isLeft)
			velocity.x += _baseSpeed * _sideWalkMul;
		if (isBack && !isForward)
			velocity.z -= _baseSpeed * _backWalkMul;

		if (isDash && !isBack)
			velocity *= _dashMul;

		velocity = transform.TransformDirection(velocity);
		_body.velocity = velocity;

		_animator.SetBool("Forward", isForward);
		_animator.SetBool("Left", isLeft);
		_animator.SetBool("Right", isRight);
		_animator.SetBool("Back", isBack);

		if (isDash)
			_animator.SetFloat("DashMultiplier", _dashMul);
		else
			_animator.SetFloat("DashMultiplier", 1.0f);

		if (isForward || isLeft || isRight || isBack || _core.shieldSystem.isHolding)
			transform.DORotateQuaternion(Quaternion.AngleAxis(_core.cameraControlSystem.angleH, Vector3.up), 0.2f);
	}
}
