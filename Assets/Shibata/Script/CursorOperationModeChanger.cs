using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorOperationMode
{
	public enum Mode
	{
		ViewportManipulate,
		FreeMovement,
	}

	private static Mode state = Mode.ViewportManipulate;
	public static Mode State
	{
		set
		{
			if (value == Mode.ViewportManipulate)
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
			if (value == Mode.FreeMovement)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
			state = value;
		}
		get
		{
			return state;
		}
	}
}


public class CursorOperationModeChanger : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		CursorOperationMode.State = CursorOperationMode.Mode.ViewportManipulate;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if (CursorOperationMode.State == CursorOperationMode.Mode.ViewportManipulate)
			{
				CursorOperationMode.State = CursorOperationMode.Mode.FreeMovement;
			}
			else if(CursorOperationMode.State==CursorOperationMode.Mode.FreeMovement)
			{
				CursorOperationMode.State = CursorOperationMode.Mode.ViewportManipulate;
			}
		}
	}
}
