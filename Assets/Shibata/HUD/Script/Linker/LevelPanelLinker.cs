using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelLinker : Lockable
{
	HUDTextPanelAccessor _panel;
	ChanCore _core;

	private void Awake()
	{
		_panel = GetComponent<HUDTextPanelAccessor>();
		_core = FindObjectOfType<ChanCore>();
	}

	private void Start()
	{
		_core.growthSystem.onLevelChanged += () =>
		  {
			  _panel.str = "Lv." + _core.growthSystem.level;
		  };
	}

	protected override void LockableUpdate()
	{
		_panel.str = "Lv." + _core.growthSystem.level;
	}
}