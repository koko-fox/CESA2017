using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HanachanMovementModule : MonoBehaviour
{
	[SerializeField]
	[Header("アニメーション用のモデル")]
	private GameObject _modelForAnimation;

	[SerializeField]
	private float _baseSpeed;
	[SerializeField]
	private float _sideWalkMultiplier;
	[SerializeField]
	private float _backWalkMultiplier;
	[SerializeField]
	private float _dashSpeedMultiplier;

	[SerializeField]
	private KeyCode _moveForwardKey = KeyCode.W;
	[SerializeField]
	private KeyCode _moveLeftKey = KeyCode.A;
	[SerializeField]
	private KeyCode _moveRightKey = KeyCode.D;
	[SerializeField]
	private KeyCode _moveBackKey = KeyCode.S;
	[SerializeField]
	private KeyCode _switchDashKey = KeyCode.LeftShift;

	/// <summary>
	/// アニメーター
	/// </summary>
	private Animator _animator;

	/// <summary>
	/// 肩越しカメラ
	/// </summary>
	private ShoulderCameraController _shoulderCam;

	private HanachanShieldControlModule _shieldControlMod;

	private Rigidbody _rigidbody;

	private bool _isInDash = false;
	public bool IsInDash { get { return _isInDash; } }

	public delegate void Trigger();

	public event Trigger OnDashEnter = delegate { };
	public event Trigger OnDashExit = delegate { };

	private void Awake()
	{
		_animator = _modelForAnimation.GetComponent<Animator>();
		_shoulderCam = FindObjectOfType<ShoulderCameraController>();
		_rigidbody = GetComponent<Rigidbody>();
		_shieldControlMod = GetComponent<HanachanShieldControlModule>();
	}

	private void Start()
	{
		OnDashEnter += () => { _isInDash = true; };
		OnDashExit += () => { _isInDash = false; };
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 velocity = Vector3.zero;
		bool isForward = Input.GetKey(_moveForwardKey);
		bool isLeft = Input.GetKey(_moveLeftKey);
		bool isRight = Input.GetKey(_moveRightKey);
		bool isBack = Input.GetKey(_moveBackKey);
		bool isDash = Input.GetKey(_switchDashKey) && !_shieldControlMod.IsHold;

		if (isForward && !isBack)
			velocity.z += _baseSpeed;
		if (isLeft && !isRight)
			velocity.x -= _baseSpeed * _sideWalkMultiplier;
		if (isRight && !isLeft)
			velocity.x += _baseSpeed * _sideWalkMultiplier;
		if (isBack && !isForward)
			velocity.z -= _baseSpeed * _backWalkMultiplier;

		if (isDash && !isBack)
			velocity *= _dashSpeedMultiplier;

		_animator.SetBool("Forward", isForward);
		_animator.SetBool("Left", isLeft);
		_animator.SetBool("Right", isRight);
		_animator.SetBool("Back", isBack);

		if (isDash)
			_animator.SetFloat("DashMultiplier", _dashSpeedMultiplier);
		else
			_animator.SetFloat("DashMultiplier", 1.0f);

		if (Input.GetKeyDown(_switchDashKey)&&!isBack)
			OnDashEnter();
		if (Input.GetKeyUp(_switchDashKey)&&!isBack)
			OnDashExit();

		velocity = transform.TransformDirection(velocity);
		_rigidbody.velocity = velocity;

		if (isForward || isLeft || isRight || isBack||_shieldControlMod.IsHold)
			transform.DORotateQuaternion(Quaternion.AngleAxis(_shoulderCam.AngleH, Vector3.up),1.5f);
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(HanachanMovementModule))]
	[CanEditMultipleObjects]
	public class HanachanMovementModuleEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			HanachanMovementModule param = target as HanachanMovementModule;

			var baseSpeed = EditorGUILayout.FloatField("ベース速度", param._baseSpeed);
			var sideWalkMul = EditorGUILayout.FloatField("横歩き速度倍率", param._sideWalkMultiplier);
			var backWalkMul = EditorGUILayout.FloatField("後ろ歩き倍率", param._backWalkMultiplier);
			var dashSpeedMul = EditorGUILayout.FloatField("ダッシュ速度倍率", param._dashSpeedMultiplier);

			var moveForwardKey = (KeyCode)EditorGUILayout.EnumPopup("前進キー", param._moveForwardKey);
			var moveLeftKey = (KeyCode)EditorGUILayout.EnumPopup("左歩きキー", param._moveLeftKey);
			var moveRightKey = (KeyCode)EditorGUILayout.EnumPopup("右歩きキー", param._moveRightKey);
			var moveBackKey = (KeyCode)EditorGUILayout.EnumPopup("後ろ歩きキー", param._moveBackKey);

			var switchDashKey = (KeyCode)EditorGUILayout.EnumPopup("ダッシュキー", param._switchDashKey);

			Undo.RecordObject(param, "HanachanMovementModuleEditor Changed");

			param._baseSpeed = baseSpeed;
			param._sideWalkMultiplier = sideWalkMul;
			param._backWalkMultiplier = backWalkMul;
			param._dashSpeedMultiplier = dashSpeedMul;

			param._moveForwardKey = moveForwardKey;
			param._moveLeftKey = moveLeftKey;
			param._moveRightKey = moveRightKey;
			param._moveBackKey = moveBackKey;

			param._switchDashKey = switchDashKey;

			EditorUtility.SetDirty(param);
		}
	}
#endif
}
