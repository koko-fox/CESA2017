using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Specter/SpecterCameraControlSystem")]
public class SpecterCameraControlSystem : Lockable
{
	[SerializeField]
	Camera _targetCamera;

	[SerializeField]
	float _mouseSensitivity = 100.0f;

	[SerializeField]
	float _maxElevation = 70.0f;

	[SerializeField]
	float _maxDepression = 70.0f;

	float _angleH = 0.0f;
	/// <summary>
	/// カメラ水平アングル
	/// </summary>
	public float angleH { get { return _angleH; } }

	float _angleV = 0.0f;
	/// <summary>
	/// カメラ水平アングル
	/// </summary>
	public float angleV { get { return _angleV; } }

	private void Awake()
	{
		if (!_targetCamera)
			_targetCamera = FindObjectOfType<Camera>();
	}

	protected override void LockableUpdate()
	{
		float deltaX = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSensitivity;
		float deltaY = Input.GetAxis("Mouse Y") * Time.deltaTime * _mouseSensitivity;

		_angleH += deltaX;
		_angleV -= deltaY;

		_angleV = Mathf.Clamp(_angleV, -_maxDepression, _maxElevation);

		Quaternion rotH = Quaternion.AngleAxis(_angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(_angleV, Vector3.right);

		transform.rotation = rotH * rotV;
		_targetCamera.transform.rotation = rotH * rotV;
		_targetCamera.transform.position = transform.position;
	}
}
