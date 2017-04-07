using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlMode
{
	public static GameObject unityChanCamera;
	public static GameObject specterCamera;

	public enum Mode
	{
		UnityChan,
		Specter,
	}

	private static Mode state = Mode.UnityChan;
	public static Mode State
	{
		set
		{
			if(value==Mode.UnityChan)
			{
				unityChanCamera.SetActive(true);
				specterCamera.SetActive(false);
			}
			if(value==Mode.Specter)
			{
				unityChanCamera.SetActive(false);
				specterCamera.SetActive(true);
			}
			state = value;
		}
		get
		{
			return state;
		}
	}
}

public class ControlModeChanger : MonoBehaviour
{
	[SerializeField]
	private GameObject unityChanCamera;

	[SerializeField]
	private GameObject specterCamera;

	// Use this for initialization
	void Start ()
	{
		ControlMode.unityChanCamera = unityChanCamera;
		ControlMode.specterCamera = specterCamera;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.U))
		{
			if (ControlMode.State == ControlMode.Mode.UnityChan)
				ControlMode.State = ControlMode.Mode.Specter;
			else
				ControlMode.State = ControlMode.Mode.UnityChan;
		}
	}
}
