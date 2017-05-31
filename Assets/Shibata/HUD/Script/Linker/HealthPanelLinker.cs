using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPanelLinker : Lockable
{
	HUDPanelAccessor _panel;

	ChanCore _chanCore;

	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_chanCore = FindObjectOfType<ChanCore>();
	}

	protected override void LockableUpdate()
	{
		_panel.SetBarScale(_chanCore.healthSystem.health / _chanCore.healthSystem.maxHealth, 0.1f);
		_panel.text = Convert.ToInt32(_chanCore.healthSystem.health).ToString() + "/" + Convert.ToInt32(_chanCore.healthSystem.maxHealth).ToString();
	}
}
