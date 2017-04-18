using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterCameraControlModule : MonoBehaviour
{
	[SerializeField]
	[Header("マウス感度")]
	private float _mouseSensitivity = 100.0f;

	[SerializeField]
	[Tooltip("仰角上限")]
	private float _maxElevation = 70.0f;

	[SerializeField]
	[Tooltip("俯角上限")]
	private float _maxDepression = 70.0f;

	private float _angleH = 0.0f;
	private float _angleV = 0.0f;
	
	// Update is called once per frame
	void Update ()
	{
		float deltaX = Input.GetAxis("Mouse X") * Time.deltaTime * _mouseSensitivity;
		float deltaY = Input.GetAxis("Mouse Y") * Time.deltaTime * _mouseSensitivity;

		_angleH += deltaX;
		_angleV -= deltaY;

		_angleV = Mathf.Clamp(_angleV, -_maxDepression, _maxElevation);

		Quaternion rotH = Quaternion.AngleAxis(_angleH, Vector3.up);
		Quaternion rotV = Quaternion.AngleAxis(_angleV, Vector3.right);
		transform.rotation = rotH * rotV;
	}
}
