using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
	#region ステータス系パラメータ
	[Header("ステータス系パラメータ設定")]
	[SerializeField]
	[Tooltip("HP最大値")]
	private float _maxHealth;
	public float MaxHealth { get { return _maxHealth; } }

	[SerializeField]
	[Tooltip("EN最大値")]
	private float _maxEnergy;
	public float MaxEnergy { get { return _maxEnergy; } }

	[SerializeField]
	[Tooltip("SP最大値")]
	private float _maxSpecial;
	public float MaxSpecial { get { return _maxSpecial; } }

	[SerializeField]
	[Tooltip("EN自然回復量(X/秒)")]
	private float _energyRegenRate;

	[SerializeField]
	[Tooltip("シールド発射コスト")]
	private float _shieldShotCost;

	[SerializeField]
	[Tooltip("シールド保持コスト(X/秒)")]
	private float _shieldRetensionCost;

	//現在HP
	private float _health;
	public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0.0f, _maxHealth); } }
	//装甲値
	private float _armorValue;
	public float ArmorValue { get { return _armorValue; } set { _armorValue = value; } }
	//エネルギー値
	private float _energyValue;
	public float EnergyValue { get { return _energyValue; } set { _energyValue = Mathf.Clamp(value, 0.0f, _maxEnergy); } }
	//SP値
	private float _specialValue;
	public float SpecialValue { get { return _specialValue; } set { _specialValue = Mathf.Clamp(value, 0.0f, _maxSpecial); } }
	//取得経験値オーブの個数
	private float _expOrbNum;
	public float ExpOrbNum { get { return _expOrbNum; } set { _expOrbNum = value; } }
	#endregion

	[Header("操作系パラメータ設定")]
	[SerializeField]
	[Tooltip("移動速度")]
	private float _forwardSpeed = 3.0f;

	[SerializeField]
	[Tooltip("後退速度")]
	private float _backSpeed = 1.5f;

	[SerializeField]
	[Tooltip("横歩き速度")]
	private float _sideWalkSpeed = 1.5f;

	[SerializeField]
	[Tooltip("マウス感度")]
	private float mouseSensitivity = 100.0f;

	[Header("アタッチするオブジェクト")]
	[SerializeField]
	[Tooltip("光の壁prefab")]
	private GameObject radiateShield;

	[SerializeField]
	[Tooltip("ターゲットにする3Dモデル")]
	private GameObject targetModel;

	[SerializeField]
	[Tooltip("カメラ")]
	private GameObject cameraObject;
	private ShoulderCameraController _shoulderCamController;

	//ユニティちゃんのアニメーター
	private Animator anim;

	//注視点
	private Vector3 lookPos;
	public Vector3 LookPos
	{
		get { return lookPos; }
	}

	private float angleH = 0.0f;
	private float angleV = 0.0f;

	/*共通化できそう*/
	private bool isInControl;
	/// <summary>
	/// コントロール中であるか
	/// </summary>
	public bool IsInControl { get { return isInControl; } }

	/// <summary>
	/// コントロールを有効化
	/// </summary>
	private void EnableControl() { isInControl = true; }
	/// <summary>
	/// コントロールを無効化
	/// </summary>
	private void DisableControl() { isInControl = false; }

	private bool isInViewportManipulate;
	/// <summary>
	/// 視点操作有効化
	/// </summary>
	private void EnableViewportManipulate() { isInViewportManipulate = true; }
	/// <summary>
	/// 視点操作無効化
	/// </summary>
	private void DisableViewportManipulate() { isInViewportManipulate = false; }

	/// <summary>
	/// カメラ操作
	/// </summary>
	private void ViewportManipulate()
	{
		//カメラ操作
		lookPos = anim.GetBoneTransform(HumanBodyBones.Head).position;
		float rotX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
		float rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

		angleH += rotX;
		angleV -= rotY;

		angleV = Mathf.Clamp(angleV, -70.0f, 70.0f);

		//クォータニオンで捻る
		Quaternion rotH = Quaternion.AngleAxis(angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(angleV, Vector3.right);
		lookPos += rotH * rotV * transform.forward;
		transform.rotation = rotH;
		cameraObject.transform.rotation = rotH * rotV;
	}

	/// <summary>
	/// 移動操作
	/// </summary>
	private void MovementControl()
	{
		Vector3 velocity = Vector3.zero;
		bool isForward = Input.GetKey(KeyCode.W);
		bool isLeft = Input.GetKey(KeyCode.A);
		bool isRight = Input.GetKey(KeyCode.D);
		bool isBack = Input.GetKey(KeyCode.S);

		if (isForward&&!isBack)
			velocity.z += _forwardSpeed;
		if (isLeft&&!isRight)
			velocity.x += -_sideWalkSpeed;
		if (isRight&&!isLeft)
			velocity.x += _sideWalkSpeed;
		if (isBack&&!isForward)
			velocity.z += -_backSpeed;

		if (isForward || isLeft || isRight || isBack)
			anim.SetBool("Forward", true);
		else
			anim.SetBool("Forward", false);

		velocity = transform.TransformDirection(velocity);
		transform.position += velocity * Time.fixedDeltaTime;
	}

	GameObject inRetensionShield = null;
	RadiateShieldController inRetensionShieldController;


	private void RadiateShieldControl()
	{
		if (Input.GetKey(KeyCode.F) && !inRetensionShield && EnergyValue >= _shieldShotCost)
		{
			inRetensionShield = Instantiate(radiateShield);
			inRetensionShieldController = inRetensionShield.GetComponent<RadiateShieldController>();
			inRetensionShieldController.CurrentMode = RadiateShieldController.Mode.Retension;
		}

		if (inRetensionShield)
		{
			inRetensionShield.transform.position = cameraObject.transform.position + cameraObject.transform.forward * 1.0f;
			inRetensionShield.transform.rotation = cameraObject.transform.rotation;
			EnergyValue -= _shieldRetensionCost * Time.deltaTime;

			if (_shieldRetensionCost*Time.deltaTime > EnergyValue)
			{
				Destroy(inRetensionShield);
				inRetensionShield = null;
			}
		}

		if (Input.GetKeyUp(KeyCode.F) && inRetensionShield)
		{
			inRetensionShieldController.CurrentMode = RadiateShieldController.Mode.Injection;
			inRetensionShield = null;
			EnergyValue -= _shieldShotCost;
		}

		if (!inRetensionShield)
			EnergyValue += _energyRegenRate * Time.deltaTime;
	}

	private void Awake()
	{
		cameraObject = FindObjectOfType<ShoulderCameraController>().gameObject;
		_shoulderCamController = cameraObject.GetComponent<ShoulderCameraController>();
	}

	void Start()
	{
		anim = targetModel.GetComponent<Animator>();
		isInControl = ControlMode.CurrentMode == ControlMode.Mode.UnityChan;
		isInViewportManipulate = CursorOperationMode.CurrentMode == CursorOperationMode.Mode.ViewportManipulate;

		ControlMode.OnChangeUnityChan += EnableControl;
		ControlMode.OnChangeSpecter += DisableControl;
		CursorOperationMode.OnChangeViewportManipulate += EnableViewportManipulate;
		CursorOperationMode.OnChangeFreeCursor += DisableViewportManipulate;
	}

	void OnDestroy()
	{
		ControlMode.OnChangeUnityChan -= EnableControl;
		ControlMode.OnChangeSpecter -= DisableControl;
		CursorOperationMode.OnChangeViewportManipulate -= EnableViewportManipulate;
		CursorOperationMode.OnChangeFreeCursor -= DisableViewportManipulate;
	}

	void FixedUpdate()
	{
		DebugTextWriter.Write("HP :" + _health + "/" + _maxHealth);
		DebugTextWriter.Write("AP :" + _armorValue);
		DebugTextWriter.Write("EN :" + _energyValue + "/" + _maxEnergy);
		DebugTextWriter.Write("SP :" + _specialValue + "/" + _maxSpecial);
		DebugTextWriter.Write("EXP:" + _expOrbNum + "個");

		if (!isInControl)
			return;

		if (isInViewportManipulate)
			//ViewportManipulate();

		MovementControl();
	}

	void Update()
	{
		if (!isInControl)
			return;
		RadiateShieldControl();

		transform.rotation = Quaternion.AngleAxis(_shoulderCamController.AngleH, Vector3.up);
	}
}
