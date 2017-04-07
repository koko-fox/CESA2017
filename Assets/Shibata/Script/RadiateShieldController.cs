using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiateShieldController : MonoBehaviour
{
	public enum Mode
	{
		Retension,	//保持
		Injection,	//射出
	}

	private Mode currentMode = Mode.Injection;
	public Mode CurrentMode
	{
		set
		{
			currentMode = value;
		}
		get
		{
			return currentMode;
		}
	}



	[SerializeField]
	[Tooltip("最大到達距離")]
	private float maximumReach=10.0f;

	[SerializeField]
	[Tooltip("移動速度")]
	private float moveSpeed=7.5f;

	//移動時間
	private float moveDuration;

	//経過時間
	private float elapsedTime = 0.0f;

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start ()
	{
		moveDuration = maximumReach / moveSpeed;
		rigidBody = transform.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (currentMode == Mode.Injection)
		{
			elapsedTime += Time.deltaTime;
			if (elapsedTime > moveDuration)
				Destroy(transform.gameObject);

			rigidBody.AddForce(transform.forward * moveSpeed * 10.0f, ForceMode.Impulse);
		}
	}
}
