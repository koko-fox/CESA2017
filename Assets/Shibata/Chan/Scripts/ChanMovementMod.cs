﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[AddComponentMenu("Chan/ChanMovementMod")]
public class ChanMovementMod : Module
{
	#region structures
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
	#endregion

	#region inputs
	ChanShieldControlMod _shieldMod;
	#endregion

	#region properties
	Rigidbody _rigidbody;
	ChanCameraControlMod _cameraControlMod;

	[SerializeField]
	GameObject _targetModel;
	Animator _animator;

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

	bool _inForwardWalk = false;
	bool _inLeftWalk = false;
	bool _inRightWalk = false;
	bool _inBackWalk = false;
	bool _inDash = false;

	[SerializeField]
	float _baseSpeed = 3.0f;
	[SerializeField]
	float _sideWalkMul = 0.75f;
	[SerializeField]
	float _backWalkMul = 0.5f;
	[SerializeField]
	float _dashMul = 1.5f;

	const string _sideWalkMulOperandName = "[in side walk]";
	const string _backWalkMulOperandName = "[in back walk]";
	const string _dashMulOperandName = "[in dash]";

	List<SpeedMultiplier> _speedMultipliers = new List<SpeedMultiplier>();
	/// <summary>移動速度乗数リスト</summary>
	public List<SpeedMultiplier> speedMultipliers
	{
		get { return _speedMultipliers; }
	}
	#endregion

	#region SpeedMultiplier methods
	public void AddSpeedMultiplier(string name,float operand)
	{
		SpeedMultiplier mul = new SpeedMultiplier(name, operand);
		_speedMultipliers.Add(mul);
	}

	public void RemoveSpeedMultiplier(string name)
	{
		_speedMultipliers.RemoveAll(mul => mul._name == name);
	}

	public Vector3 ProcessSpeed(Vector3 value)
	{
		var ret = value;
		foreach (var elem in _speedMultipliers)
			ret *= elem._operand;
		return ret;
	}
	#endregion

	#region begin/end methods
	void BeginForwardWalk()
	{
		_inForwardWalk = true;
	}

	void BeginLeftWalk()
	{
		AddSpeedMultiplier(_sideWalkMulOperandName, _sideWalkMul);
		_inLeftWalk = true;
	}

	void BeginRightWalk()
	{
		AddSpeedMultiplier(_sideWalkMulOperandName, _sideWalkMul);
		_inRightWalk = true;
	}
	void BeginBackWalk()
	{
		AddSpeedMultiplier(_backWalkMulOperandName, _backWalkMul);
		_inBackWalk = true;
	}

	void BeginDash()
	{
		AddSpeedMultiplier(_dashMulOperandName, _dashMul);
		_inDash = true;
	}

	void EndForwardWalk()
	{
		_inForwardWalk = false;
	}

	void EndLeftWalk()
	{
		RemoveSpeedMultiplier(_sideWalkMulOperandName);
		_inLeftWalk = false;
	}

	void EndRightWalk()
	{
		RemoveSpeedMultiplier(_sideWalkMulOperandName);
		_inRightWalk = false;
	}

	void EndBackWalk()
	{
		RemoveSpeedMultiplier(_backWalkMulOperandName);
		_inBackWalk = false;
	}

	void EndDash()
	{
		RemoveSpeedMultiplier(_dashMulOperandName);
		_inDash = false;
	}
	#endregion

	#region override methods
	protected override void ModuleAwake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_animator = _targetModel.GetComponent<Animator>();
		_cameraControlMod = GetComponent<ChanCameraControlMod>();
		_shieldMod = GetComponent<ChanShieldControlMod>();
	}

	public override void OrdableFixedUpdate()
	{
		base.OrdableFixedUpdate();

		if (Input.GetKeyDown(_forwardKey)&&!_inBackWalk&&!_inForwardWalk)
			BeginForwardWalk();
		if (Input.GetKeyUp(_forwardKey)&&_inForwardWalk)
			EndForwardWalk();

		if (Input.GetKeyDown(_leftKey) && !_inRightWalk&&!_inLeftWalk)
			BeginLeftWalk();
		if (Input.GetKeyUp(_leftKey)&&_inLeftWalk)
			EndLeftWalk();

		if (Input.GetKeyDown(_rightKey) && !_inLeftWalk&&!_inRightWalk)
			BeginRightWalk();
		if (Input.GetKeyUp(_rightKey)&&_inRightWalk)
			EndRightWalk();

		if (Input.GetKeyDown(_backKey) && !_inForwardWalk && !_inBackWalk)
			BeginBackWalk();
		if (Input.GetKeyUp(_backKey) && _inBackWalk)
			EndBackWalk();

		if (Input.GetKeyDown(_dashKey)&&!_inDash)
			BeginDash();
		if (Input.GetKeyUp(_dashKey)&&_inDash)
			EndDash();

		Vector3 velocity = Vector3.zero;

		if (_inForwardWalk)
			velocity.z += _baseSpeed;
		if (_inLeftWalk)
			velocity.x -= _baseSpeed;
		if (_inRightWalk)
			velocity.x += _baseSpeed;
		if (_inBackWalk)
			velocity.z -= _baseSpeed;

		velocity = ProcessSpeed(velocity);
		velocity = transform.TransformDirection(velocity);
		_rigidbody.velocity = velocity;

		_animator.SetBool("isRun", _inForwardWalk || _inLeftWalk || _inRightWalk);
		_animator.SetFloat("Speed", velocity.magnitude/_baseSpeed);

		if(_inForwardWalk||_inLeftWalk||_inRightWalk||_inBackWalk||_shieldMod.state== ChanShieldControlMod.State.Hold)
		{
			transform.DORotateQuaternion(Quaternion.AngleAxis(_cameraControlMod.angleH,
				Vector3.up), 0.2f);
		}
	}
	#endregion
}