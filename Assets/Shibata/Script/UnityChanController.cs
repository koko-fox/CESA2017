using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("ターゲットにする3Dモデル")]
	private GameObject targetModel;

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
	[Tooltip("カメラ")]
	private Camera camera;

	[SerializeField]
	[Tooltip("マウス感度")]
	private float mouseSensitivity = 100.0f;

	[SerializeField]
	[Tooltip("光の壁prefab")]
	private GameObject radiateShield;

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


	void Start()
	{
		anim = targetModel.GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (ControlMode.State != ControlMode.Mode.UnityChan)
			return;

		if (CursorOperationMode.State == CursorOperationMode.Mode.ViewportManipulate)
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

		//光の壁を出す
		if (Input.GetKeyDown(KeyCode.F))
		{
			var shield = Instantiate(radiateShield);
			shield.transform.position = transform.position + transform.forward * 1.0f;
			shield.transform.rotation = transform.rotation;
		}

		DebugTextWriter.Write("ユニティちゃんの位置:" + transform.position.ToString());
		DebugTextWriter.Write("ユニティちゃんの角度:" + transform.rotation.ToString());
	}
}
