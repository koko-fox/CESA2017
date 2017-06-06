using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

public class ChanCoreSystem : Core
{

	ChanFacade _facade;

	DebugBoardRegister.Panel _panel;

	void Awake()
	{
		_panel = DebugBoardRegister.CreatePanel();
	}

	protected override void Start()
	{
		base.Start();
		_facade = GetComponent<ChanFacadeHolder>().facade;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		var growth = _facade.growthMod;
		StringBuilder buf = new StringBuilder(1024);

		foreach(var elem in _modules)
		{
			buf.Append(elem.GetType().ToString());
			if (elem.locked)
				buf.Append(":<color=#ff0000>locked</color>\n");
			else
				buf.Append(":<color=#00ff00>unlocked</color>\n");
		}
		buf.Append("-----------------\n");
		buf.AppendFormat("require exp:{0}\n", growth.requireExpForLevelUp);
		buf.AppendFormat("combo count:{0}combo\n", growth.comboCount);
		buf.AppendFormat("combo remain:{0}[sec]\n", growth.comboDiscardElapsed);
		buf.AppendFormat("exp multiplier:x{0}\n", growth.expMultiplierFromCombo);
		_panel.text = "<color=#ffffff>" + buf.ToString() + "</color>";
	}
}
