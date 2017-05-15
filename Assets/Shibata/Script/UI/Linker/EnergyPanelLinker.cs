using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPanelLinker : Lockable
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
		_chanCore.energySystem.onEnergyChanged += () =>
		  {
			  float ratio = _chanCore.energySystem.energy / _chanCore.energySystem.maxEnergy;
			  _panel.SetBarScale(ratio, 0.5f);
			  _panel.SetValue(_chanCore.energySystem.energy, 0.5f);
		  };
	}
}
