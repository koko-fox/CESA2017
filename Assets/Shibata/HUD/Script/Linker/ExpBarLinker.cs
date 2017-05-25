using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarLinker : Lockable
{
	HUDPanelAccessor _panel;
	ChanCore _core;

	HUDDebugMessagePanelAccessor _debug;

	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_debug = FindObjectOfType<HUDDebugMessagePanelAccessor>();
		_core = FindObjectOfType<ChanCore>();
	}

	void Start ()
	{
		_core.growthSystem.onExpChanged += () =>
		 {
			 var sys = _core.growthSystem;

			 float left = sys.exp - sys.prevRequireExp;
			 float mem = sys.nextRequireExp - sys.prevRequireExp;

			 if (left/mem<0)
				 return;

			 _panel.SetBarScale(left / mem, 0.0f);
		 };
	}

	private void FixedUpdate()
	{
		/*
		var sys = _core.growthSystem;
		_debug.message += "exp:" + sys.exp + "\n";
		_debug.message += "next:" + sys.nextRequireExp + "\n";

		var info = _core.GetLockInfo();
		foreach(var elem in info)
		{
			_debug.message += elem.name + ":" + elem.locked.ToString() + "\n";
		}
		*/
	}
}
