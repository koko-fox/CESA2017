using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectorCameraController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("マウス感度")]
	private float mouseSensitivity;

	[SerializeField]
	[Tooltip("移動速度")]
	private float moveSpeed;

	[SerializeField]
	private GameObject[] _items;

	private int _selectedIndex = 0;

	private float angleH;
	private float angleV;

	private Camera camera;

	private bool isInControl;
	private void EnableControl() { isInControl = true; }
	private void DisableControl() { isInControl = false; }

	private bool isEnableCameraControl;
	private void EnableCameraControl() { isEnableCameraControl = true; }
	private void DisableCameraControl() { isEnableCameraControl = false; }

	/// <summary>
	/// カメラ操作
	/// </summary>
	private void CameraControl()
	{
		float rotX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
		float rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

		angleH += rotX;
		angleV -= rotY;

		angleV = Mathf.Clamp(angleV, -70.0f, 70.0f);

		//クォータニオンで捻る
		Quaternion rotH = Quaternion.AngleAxis(angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(angleV, Vector3.right);
		transform.rotation = rotH * rotV;
	}

	/// <summary>
	/// 移動操作
	/// </summary>
	private void MovementControl()
	{
		//前進
		if (Input.GetKey(KeyCode.W))
		{
			Vector3 vel = new Vector3(0, 0, moveSpeed);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.deltaTime;
		}
		//左移動
		else if (Input.GetKey(KeyCode.A))
		{
			Vector3 vel = new Vector3(-moveSpeed, 0, 0);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.deltaTime;
		}
		//右移動
		else if (Input.GetKey(KeyCode.D))
		{
			Vector3 vel = new Vector3(moveSpeed, 0, 0);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.deltaTime;
		}
		//後退
		else if (Input.GetKey(KeyCode.S))
		{
			Vector3 vel = new Vector3(0, 0, -moveSpeed);
			vel = transform.TransformDirection(vel);
			transform.localPosition += vel * Time.deltaTime;
		}
	}

	/// <summary>
	/// アイテム生成操作
	/// </summary>
	private void ItemConstructControl()
	{
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
		Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

		if (Input.GetKeyDown(KeyCode.F))
		{
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				var obj = Instantiate(_items[_selectedIndex]);
				obj.transform.position = hit.point;
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
		camera = GetComponent<Camera>();

		isInControl = ControlMode.CurrentMode == ControlMode.Mode.Specter;
		isEnableCameraControl = CursorOperationMode.CurrentMode == CursorOperationMode.Mode.ViewportManipulate;

		ControlMode.OnChangeSpecter += EnableControl;
		ControlMode.OnChangeUnityChan += DisableControl;
		CursorOperationMode.OnChangeViewportManipulate += EnableCameraControl;
		CursorOperationMode.OnChangeFreeCursor += DisableCameraControl;
	}

	void OnDestroy()
	{
		ControlMode.OnChangeSpecter -= EnableControl;
		ControlMode.OnChangeUnityChan -= DisableControl;
		CursorOperationMode.OnChangeViewportManipulate -= EnableCameraControl;
		CursorOperationMode.OnChangeFreeCursor -= DisableCameraControl;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isInControl)
			return;
		if(isEnableCameraControl)
			CameraControl();

		if (Input.GetKeyDown(KeyCode.Alpha1))
			_selectedIndex = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2))
			_selectedIndex = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3))
			_selectedIndex = 2;
		if (Input.GetKeyDown(KeyCode.Alpha4))
			_selectedIndex = 3;
		if (Input.GetKeyDown(KeyCode.Alpha5))
			_selectedIndex = 4;

		MovementControl();
		ItemConstructControl();
	}

	private void FixedUpdate()
	{
		DebugTextWriter.Write("select item:" + _items[_selectedIndex].name);
	}
}
