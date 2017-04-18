using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class CursorOperationModeChanger : MonoBehaviour
{
	private ControlModeChanger _controlModeChanger;

	private HanachanCore _hanachanCore;
	private ShoulderCameraController _shoulderCam;
	private SpecterCore _specterCore;

	private bool _isCameraControlEnabled = true;
	private bool _isFreeCursorEnabled = false;

	private void Awake()
	{
		_controlModeChanger = FindObjectOfType<ControlModeChanger>();
		_hanachanCore = FindObjectOfType<HanachanCore>();
		_shoulderCam = FindObjectOfType<ShoulderCameraController>();
		_specterCore = FindObjectOfType<SpecterCore>();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if(_isCameraControlEnabled)
			{
				if (_controlModeChanger.IsHanachanEnabled)
				{
					_hanachanCore.MovementModule.enabled = false;
					_hanachanCore.ShieldControlModule.enabled = false;
					_shoulderCam.enabled = false;
				}
				else
				{
					_specterCore.MovementModule.enabled = false;
					_specterCore.CameraControlModule.enabled = false;
				}
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				_isCameraControlEnabled = false;
				_isFreeCursorEnabled = true;
			}
			else
			{
				if (_controlModeChanger.IsHanachanEnabled)
				{
					_hanachanCore.MovementModule.enabled = true;
					_hanachanCore.ShieldControlModule.enabled = true;
					_shoulderCam.enabled = true;
				}
				else
				{
					_specterCore.MovementModule.enabled = true;
					_specterCore.CameraControlModule.enabled = true;
				}
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				_isCameraControlEnabled = true;
				_isFreeCursorEnabled = false;
			}

			/*
			if (CursorOperationMode.CurrentMode == CursorOperationMode.Mode.ViewportManipulate)
			{
				CursorOperationMode.CurrentMode = CursorOperationMode.Mode.FreeCursor;
			}
			else if(CursorOperationMode.CurrentMode==CursorOperationMode.Mode.FreeCursor)
			{
				CursorOperationMode.CurrentMode = CursorOperationMode.Mode.ViewportManipulate;
			}
			*/
		}
	}
}
