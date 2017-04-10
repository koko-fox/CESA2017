using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangeUnityChan();
public delegate void OnChangeSpecter();

public static class ControlMode
{
	/// <summary>
	/// ユニティちゃんにコントロールが移った時のdelegate
	/// </summary>
	public static OnChangeUnityChan OnChangeUnityChan { get; set; }

	/// <summary>
	/// スペクターにコントロールが移った時のdelegate
	/// </summary>
	public static OnChangeSpecter OnChangeSpecter { get; set; }
	public static GameObject unityChanCamera;
	public static GameObject specterCamera;

	public enum Mode
	{
		UnityChan,
		Specter,
	}

	private static Mode currentMode;
	public static Mode CurrentMode
	{
		set
		{
			if(value==Mode.UnityChan)
			{
				OnChangeUnityChan();
				unityChanCamera.SetActive(true);
				specterCamera.SetActive(false);
			}
			if(value==Mode.Specter)
			{
				OnChangeSpecter();
				unityChanCamera.SetActive(false);
				specterCamera.SetActive(true);
			}
			currentMode = value;
		}
		get
		{
			return currentMode;
		}
	}
}

public class ControlModeChanger : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		ControlMode.unityChanCamera = FindObjectOfType<CameraController>().gameObject;
		ControlMode.specterCamera = FindObjectOfType<SpectorCameraController>().gameObject;
		ControlMode.CurrentMode = ControlMode.Mode.UnityChan;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.U))
		{
			if (ControlMode.CurrentMode == ControlMode.Mode.UnityChan)
				ControlMode.CurrentMode = ControlMode.Mode.Specter;
			else
				ControlMode.CurrentMode = ControlMode.Mode.UnityChan;
		}
	}
}
