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
	private float forwardSpeed = 3.0f;

	[SerializeField]
	[Tooltip("後退速度")]
	private float backSpeed = 1.5f;

	[SerializeField]
	[Tooltip("横歩き速度")]
	private float sideWalkSpeed = 1.5f;

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
	private Camera camera;

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
		camera.transform.rotation = rotH * rotV;
	}

	/// <summary>
	/// 移動操作
	/// </summary>
	private void MovementControl()
	{
		//前進
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Forward", true);
			Vector3 vel = new Vector3(0, 0, forwardSpeed);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.fixedDeltaTime;
		}
		//左移動
		else if (Input.GetKey(KeyCode.A))
		{
			anim.SetBool("Forward", true);
			Vector3 vel = new Vector3(-sideWalkSpeed, 0, 0);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.fixedDeltaTime;
		}
		//右移動
		else if (Input.GetKey(KeyCode.D))
		{
			anim.SetBool("Forward", true);
			Vector3 vel = new Vector3(sideWalkSpeed, 0, 0);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.fixedDeltaTime;
		}
		else
		{
			anim.SetBool("Forward", false);
		}

		//後退
		if (Input.GetKey(KeyCode.S))
		{
			anim.SetBool("Back", true);
			Vector3 vel = new Vector3(0, 0, -backSpeed);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.fixedDeltaTime;

		}
		else
		{
			anim.SetBool("Back", false);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool("DownSpaceKey", true);
		}
		else
		{
			anim.SetBool("DownSpaceKey", false);
		}
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
			inRetensionShield.transform.position = camera.transform.position + camera.transform.forward * 1.0f;
			inRetensionShield.transform.rotation = camera.transform.rotation;
			EnergyValue -= _shieldRetensionCost * Time.fixedDeltaTime;

			if (_shieldRetensionCost*Time.fixedDeltaTime > EnergyValue)
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
			ViewportManipulate();

		MovementControl();
		RadiateShieldControl();

		if(!inRetensionShield)
			EnergyValue += _energyRegenRate * Time.fixedDeltaTime;
	}
}
