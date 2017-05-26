using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPanelLinker : Lockable
{
	HUDPanelAccessor _panel;
	ChanCore _core;
	ChanBurstSystem _burstSystem;

	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_core = FindObjectOfType<ChanCore>();
	}

	private void Start()
	{
		_burstSystem = _core.burstSystem;
	}

	protected override void LockableUpdate()
	{
		_panel.text = _burstSystem.killCount + "/" + _burstSystem.requireKillCount;
		_panel.SetBarScale(_burstSystem.killCount / (float)_burstSystem.requireKillCount, 0.1f);
	}
}
