using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public delegate void OnChangeViewportManipulate();
public delegate void OnChangeFreeCursor();

public static class CursorOperationMode
{
	public enum Mode
	{
		ViewportManipulate, //視点操作モード
		FreeCursor,     //自由カーソルモード
	}

	private static OnChangeViewportManipulate onChangeViewportManipulate;
	/// <summary>
	/// 視点操作モード変更時に呼び出し
	/// </summary>
	public static OnChangeViewportManipulate OnChangeViewportManipulate { get; set; }
	private static OnChangeFreeCursor onChangeFreeCursor;
	/// <summary>
	/// 自由カーソルモード変更時に呼び出し
	/// </summary>
	public static OnChangeFreeCursor OnChangeFreeCursor { get; set; }

	private static Mode currentMode = Mode.ViewportManipulate;
	/// <summary>
	/// 現在のモード
	/// </summary>
	public static Mode CurrentMode
	{
		set
		{
			if (value == Mode.ViewportManipulate)
			{
				OnChangeViewportManipulate();
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
			if (value == Mode.FreeCursor)
			{
				OnChangeFreeCursor();
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
			currentMode = value;
		}
		get
		{
			return currentMode;
		}
	}
}


public class CursorOperationModeChanger : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		CursorOperationMode.CurrentMode = CursorOperationMode.Mode.ViewportManipulate;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if (CursorOperationMode.CurrentMode == CursorOperationMode.Mode.ViewportManipulate)
			{
				CursorOperationMode.CurrentMode = CursorOperationMode.Mode.FreeCursor;
			}
			else if(CursorOperationMode.CurrentMode==CursorOperationMode.Mode.FreeCursor)
			{
				CursorOperationMode.CurrentMode = CursorOperationMode.Mode.ViewportManipulate;
			}
		}
	}
}
