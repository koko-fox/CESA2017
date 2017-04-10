using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	[Tooltip("ユニティちゃんのコントローラ")]
	private UnityChanController controller;

	[SerializeField]
	[Tooltip("")]
	private float cameraDistance = 0.0f;

	private Transform playerHead;

	void Start()
	{
		controller = FindObjectOfType<UnityChanController>();
		var animator = controller.GetComponentInChildren<Animator>();
		playerHead = animator.GetBoneTransform(HumanBodyBones.Head);
	}

	void Update()
	{
		transform.position = playerHead.transform.position + playerHead.transform.up * cameraDistance;
		//transform.LookAt(controller.LookPos);
	}
}
