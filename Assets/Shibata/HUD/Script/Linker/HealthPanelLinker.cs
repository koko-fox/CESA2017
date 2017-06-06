using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPanelLinker : MonoBehaviour
{
	#region inputs
	HUDPanelAccessor _panel;
	ChanHealthMod _healthMod;
	#endregion

	#region unity methods
	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_healthMod = FindObjectOfType<ChanFacadeHolder>().facade.healthMod;
	}
	private void Start()
	{
		_healthMod.onHealthChanged += () =>
		  {
			  _panel.SetBarScale(_healthMod.health / _healthMod.maxHealth, 0.1f);
			  _panel.text = Convert.ToInt32(_healthMod.health).ToString() + "/"
			  + Convert.ToInt32(_healthMod.maxHealth).ToString();
		  };
		_panel.SetBarScale(_healthMod.health / _healthMod.maxHealth, 0.1f);
		_panel.text = Convert.ToInt32(_healthMod.health).ToString() + "/"
			  + Convert.ToInt32(_healthMod.maxHealth).ToString();
	}
	#endregion
}
