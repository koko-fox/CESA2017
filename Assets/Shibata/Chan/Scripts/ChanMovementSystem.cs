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
	const string _dashMulOperandName = "InDash";

	public struct SpeedMultiplier
	{
		public string _name;
		public float _operand;

		public SpeedMultiplier(string name,float operand)
		{
			_name = name;
			_operand = operand;
		}
	}

	List<SpeedMultiplier> _speedMultipliers;
	public List<SpeedMultiplier> SpeedMultipliers
	{
		get
		{
			return _speedMultipliers;
		}
	}

	/// <summary>
	/// 乗数を追加する
	/// </summary>
	/// <param name="name">演算数の名前</param>
	/// <param name="operand">演算数</param>
	public void AddSpeedMultiplier(string name,float operand)
	{
		SpeedMultiplier mul = new SpeedMultiplier(name, operand);
		_speedMultipliers.Add(mul);
	}

	/// <summary>
	/// 乗数を削除する
	/// </summary>
	/// <param name="name">削除する演算数の名前</param>
	public void RemoveSpeedMultiplier(string name)
	{
		_speedMultipliers.RemoveAll(mul => mul._name == name);
	}

	/// <summary>
	/// 速度を加工する
	/// </summary>
	/// <param name="value">加工する速度</param>
	/// <returns>加工された速度</returns>
	Vector3 ProcessSpeed(Vector3 value)
	{
		Vector3 ret = value;
		foreach(var elem in _speedMultipliers)
		{
			ret *= elem._operand;
		}

		return ret;
	}

	float ProcessSpeed(float value)
	{
		float ret = value;
		foreach(var elem in _speedMultipliers)
		{
			ret *= elem._operand;
		}
		return ret;
	}

	KeyCode _forwardKey = KeyCode.W;
	[SerializeField]
	KeyCode _leftKey = KeyCode.A;
	[SerializeField]
	KeyCode _rightKey = KeyCode.D;
	[SerializeField]
	KeyCode _backKey = KeyCode.S;

	[SerializeField]
	KeyCode _dashKey = KeyCode.LeftShift;
	bool _isDash;

	void BeginDash()
	{
		AddSpeedMultiplier(_dashMulOperandName, _dashMul);
		_isDash = true;
	}

	void EndDash()
	{
		RemoveSpeedMultiplier(_dashMulOperandName);
		_isDash = false;
	}

	bool _dashLock = false;
	/// <summary>
	/// ダッシュ機能のロックフラグ
	/// </summary>
	public bool dashLock
	{
		get { return _dashLock; }
		set
		{
			_dashLock = value;
			if (_dashLock)
				EndDash();
		}
	}

	private void Awake()
	{
		_speedMultipliers = new List<SpeedMultiplier>();

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

		if (Input.GetKeyDown(_dashKey) && !dashLock&&!_isDash)
			BeginDash();
		
		if(Input.GetKeyUp(_dashKey))
			EndDash();

		if (isForward && !isBack)
			velocity.z += _baseSpeed;
		if (isLeft && !isRight)
			velocity.x -= _baseSpeed * _sideWalkMul;
		if (isRight && !isLeft)
			velocity.x += _baseSpeed * _sideWalkMul;
		if (isBack && !isForward)
			velocity.z -= _baseSpeed * _backWalkMul;

		velocity = ProcessSpeed(velocity);

		velocity = transform.TransformDirection(velocity);
		_body.velocity = velocity;
	
		_animator.SetBool("isRun", isForward || isLeft || isRight);
		_animator.SetFloat("Speed", ProcessSpeed(1.0f));

		if (isForward || isLeft || isRight || isBack || _core.shieldSystem.isHolding)
			transform.DORotateQuaternion(Quaternion.AngleAxis(_core.cameraControlSystem.angleH, Vector3.up), 0.2f);
	}

	
}
