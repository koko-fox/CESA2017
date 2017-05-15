using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPanelLinker : Lockable
{
	HUDPanelAccessor _panel;

	ChanCore _chanCore;

	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_chanCore = FindObjectOfType<ChanCore>();
	}

	private void Start()
	{
		_chanCore.healthSystem.onHealthChanged += () =>
		{
			float ratio = _chanCore.healthSystem.health / _chanCore.healthSystem.MaxHealth;
			_panel.SetBarScale(ratio, 0.5f);
			_panel.SetValue(_chanCore.healthSystem.health, 0.5f);
		};
	}
}
