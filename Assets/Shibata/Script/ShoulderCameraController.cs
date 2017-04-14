using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderCameraController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("カメラ距離")]
	private float _distance = 1.0f;

	[SerializeField]
	[Tooltip("カメラ高さ")]
	private float _height;

	[SerializeField]
	[Tooltip("カメラ水平位置")]
	[Range(-1.0f, 1.0f)]
	private float _horizontalPosition = 0.0f;

	[SerializeField]
	[Tooltip("被写体")]
	private GameObject _target;

	[SerializeField]
	[Tooltip("マウス感度")]
	private float _mouseSensitivity = 100.0f;

	[SerializeField]
	[Tooltip("仰角上限")]
	private float _maxElevation = 70.0f;

	[SerializeField]
	[Tooltip("俯角上限")]
	private float _maxDepression = 70.0f;

	//垂直角度
	private float _angleV = 0.0f;
	public float AngleV { get { return _angleV; } }
	//水平角度
	private float _angleH = 0.0f;
	public float AngleH { get { return _angleH; } }
	
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

		Vector3 manip = Vector3.zero;
		manip -= transform.forward * _distance;
		manip.y += _height;
		manip += transform.right * _horizontalPosition;

		transform.position = _target.transform.position;
		transform.position += manip;
	}
}
