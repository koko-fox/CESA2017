using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanCameraControlSystem")]
/// <summary>
/// カメラ操作機能を追加するモジュール
/// </summary>
public class ChanCameraControlSystem : Lockable
{
	[SerializeField]
	Camera _camera;

	[SerializeField]
	float _distance = 1.0f;
	[SerializeField]
	float _height;
	[SerializeField]
	float _horizontalPos = 0.0f;

	[SerializeField]
	float _mouseSensitivity = 100.0f;

	[SerializeField]
	float _maxElevation = 70.0f;
	[SerializeField]
	float _maxDepression = 70.0f;

	float _angleV = 0.0f;
	/// <summary>
	/// 垂直角度
	/// </summary>
	public float angleV { get { return _angleV; } }

	float _angleH = 0.0f;
	public float angleH { get { return _angleH; } }

	private void Awake()
	{
		if (!_camera)
			_camera = FindObjectOfType<Camera>();
	}

	protected override void LockableUpdate()
	{
		float deltaX = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSensitivity;
		float deltaY = Input.GetAxis("Mouse Y") *
			Time.deltaTime * _mouseSensitivity;

		_angleH += deltaX;
		_angleV -= deltaY;

		_angleV = Mathf.Clamp(_angleV, -_maxDepression, _maxElevation);

		Quaternion rotH = Quaternion.AngleAxis(_angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(_angleV, Vector3.right);
		_camera.transform.rotation = rotH * rotV;

		Vector3 manip = Vector3.zero;
		manip -= _camera.transform.forward * _distance;
		manip.y += _height;
		manip += _camera.transform.right * _horizontalPos;

		_camera.transform.position = transform.position;
		_camera.transform.position += manip;
	}
}
