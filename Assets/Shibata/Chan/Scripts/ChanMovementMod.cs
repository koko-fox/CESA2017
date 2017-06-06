using System.Collections;
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
		_targetModel.transform.DOComplete();
		_targetModel.transform.DOLocalRotate(new Vector3(0.0f, -45.0f), 0.2f);
		_inLeftWalk = true;
	}

	void BeginRightWalk()
	{
		AddSpeedMultiplier(_sideWalkMulOperandName, _sideWalkMul);
		_targetModel.transform.DOComplete();
		_targetModel.transform.DOLocalRotate(new Vector3(0.0f, 45.0f), 0.2f);
		_inRightWalk = true;
	}
	void BeginBackWalk()
	{
		AddSpeedMultiplier(_backWalkMulOperandName, _backWalkMul);
		_targetModel.transform.DOComplete();
		_targetModel.transform.DOLocalRotate(new Vector3(0.0f, 180.0f), 0.2f);
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
		_targetModel.transform.DOComplete();
		_targetModel.transform.DOLocalRotate(new Vector3(0.0f, 0.0f), 0.2f);
		_inLeftWalk = false;
	}

	void EndRightWalk()
	{
		RemoveSpeedMultiplier(_sideWalkMulOperandName);
		_targetModel.transform.DOComplete();
		_targetModel.transform.DOLocalRotate(new Vector3(0.0f, 0.0f), 0.2f);
		_inRightWalk = false;
	}

	void EndBackWalk()
	{
		RemoveSpeedMultiplier(_backWalkMulOperandName);
		_targetModel.transform.DOComplete();
		_targetModel.transform.DOLocalRotate(new Vector3(0.0f, 0.0f), 0.2f);
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
		bool forward = Input.GetKey(_forwardKey);
		bool left = Input.GetKey(_leftKey);
		bool right = Input.GetKey(_rightKey);
		bool back = Input.GetKey(_backKey);
		bool dash = Input.GetKey(_dashKey);

		if (forward && !back&&!_inForwardWalk)
			BeginForwardWalk();
		if ((!forward||back) && _inForwardWalk)
			EndForwardWalk();

		if (back && !forward&&!_inBackWalk)
			BeginBackWalk();
		if ((!back||forward) && _inBackWalk)
			EndBackWalk();

		if (left && !right&&!_inLeftWalk)
			BeginLeftWalk();
		if ((!left||right) && _inLeftWalk)
			EndLeftWalk();

		if (right && !left&&!_inRightWalk)
			BeginRightWalk();
		if ((!right||left) && _inRightWalk)
			EndRightWalk();

		if (dash && !_inDash)
			BeginDash();
		if (!dash && _inDash)
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

		_animator.SetBool("isRun", _inForwardWalk || _inLeftWalk || _inRightWalk||_inBackWalk);
		_animator.SetFloat("Speed", velocity.magnitude/_baseSpeed);

		if(_inForwardWalk||_inLeftWalk||_inRightWalk||_inBackWalk||_shieldMod.state== ChanShieldControlMod.State.Hold)
		{
			transform.DORotateQuaternion(Quaternion.AngleAxis(_cameraControlMod.angleH,
				Vector3.up), 0.2f);
		}
	}
	#endregion
}
