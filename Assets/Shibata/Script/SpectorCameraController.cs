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
	private GameObject item;

	private float angleH;
	private float angleV;

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (ControlMode.State != ControlMode.Mode.Specter)
			return;

		float rotX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
		float rotY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

		angleH += rotX;
		angleV -= rotY;

		angleV = Mathf.Clamp(angleV, -70.0f, 70.0f);

		//クォータニオンで捻る
		Quaternion rotH = Quaternion.AngleAxis(angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(angleV, Vector3.right);
		transform.rotation = rotH * rotV;

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

		Camera camera = GetComponent<Camera>();
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
		Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

		if (Input.GetKeyDown(KeyCode.F))
		{
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				var obj = Instantiate(item);
				obj.transform.position = hit.point;
			}
		}
	}
}
