using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HanachanController : MonoBehaviour
{
	[SerializeField]
	[Header("操作するモデル")]
	private GameObject _operateModel;

	private void Awake()
	{
		_animator = _operateModel.GetComponent<Animator>();
		_shoulderCam = FindObjectOfType<ShoulderCameraController>();
	}

	// Use this for initialization
	void Start ()
	{
	}

	private void FixedUpdate()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		MovementControl();
	}
}
