using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotary : MonoBehaviour
{
	[SerializeField]
	private float _rotationVelocity;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(new Vector3(0.0f,0.0f,_rotationVelocity * Time.deltaTime));
	}
}
