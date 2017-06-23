using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public static class TFCursor
{
	#region private fields
	static bool _initialized = false;
	static bool _enabled;
	static ChanCoreSystem _chan;
	#endregion

	#region properties
	public static bool enabled
	{
		get { return _enabled; }
	}
	#endregion

	#region private methods
	static void Initialize()
	{
		_chan = GameObject.FindObjectOfType<ChanCoreSystem>();
		_initialized = true;
	}
	#endregion

	#region public methods
	/// <summary>カーソルを有効化する</summary>
	public static void Enable()
	{
		if (!_initialized)
			Initialize();

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		if (_chan)
		{
			foreach (var elem in _chan.modules)
			{
				elem.locked = true;
			}
		}
		else
		{
			TFDebug.Log("@system", "Chanの無効化に失敗\nChanCoreSystemが見つかりませんでした");
		}
		_enabled = true;
	}

	/// <summary>カーソルを無効化する</summary>
	public static void Disable()
	{
		if (!_initialized)
			Initialize();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		if (_chan)
		{
			foreach (var elem in _chan.modules)
			{
				elem.locked = false;
			}
		}
		else
		{
			TFDebug.Log("@system", "Chanの有効化に失敗\nChanCoreSystemが見つかりませんでした");
		}
		_enabled = false;
	}
	#endregion
}

public class CursorOperationModeChanger : MonoBehaviour
{
	#region unity methods
	void Awake()
	{
		TFCursor.Disable();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			if(TFCursor.enabled)
			{
				TFCursor.Disable();
			}
			else
			{
				TFCursor.Enable();
			}
		}
	}
	#endregion
}