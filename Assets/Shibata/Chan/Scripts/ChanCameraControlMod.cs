using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanCameraControlMod")]
public class ChanCameraControlMod : Module
{
	#region properties
	[SerializeField]
	Camera _mainCamera;
	[SerializeField]
	float _distance = 1.0f;
	[SerializeField]
	float _height = 1.4f;
	[SerializeField]
	float _horizontalPos = 0.0f;
	[SerializeField]
	float _mouseSensitivity = 100.0f;
	[SerializeField]
	float _maxElevation = 45.0f;
	[SerializeField]
	float _maxDepression = 45.0f;

	float _angleV = 0.0f;
	/// <summary>垂直角度</summary>
	public float angleV { get { return _angleV; } }

	float _angleH = 0.0f;
	/// <summary>水平角度</summary>
	public float angleH { get { return _angleH; } }
	#endregion

	#region methods
	protected override void ModuleAwake()
	{
		if (!_mainCamera)
			_mainCamera = FindObjectOfType<Camera>();
	}

	public override void OrdableUpdate()
	{
		float deltaX = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSensitivity;
		float deltaY = Input.GetAxis("Mouse Y") *
			Time.deltaTime * _mouseSensitivity;

		_angleH += deltaX;
		_angleV -= deltaY;

		_angleV = Mathf.Clamp(_angleV, -_maxDepression, _maxElevation);

		Quaternion rotH = Quaternion.AngleAxis(_angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(_angleV, Vector3.right);
		_mainCamera.transform.rotation = rotH * rotV;

		Vector3 manip = Vector3.zero;
		manip -= _mainCamera.transform.forward * _distance;
		manip.y += _height;
		manip += _mainCamera.transform.right * _horizontalPos;

		_mainCamera.transform.position = transform.position;
		_mainCamera.transform.position += manip;
	}
	#endregion
}
