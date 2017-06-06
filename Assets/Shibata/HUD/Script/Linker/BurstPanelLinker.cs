using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPanelLinker : MonoBehaviour
{
	#region inputs
	HUDPanelAccessor _panel;
	ChanBurstMod _burstMod;
	#endregion

	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_burstMod = FindObjectOfType<ChanBurstMod>();
	}

	private void Start()
	{
		_burstMod.onChangedKillCount += () =>
		  {
			  _panel.text = _burstMod.killCount + "/" + _burstMod.requireKillCount;
			  _panel.SetBarScale(_burstMod.killCount / _burstMod.requireKillCount, 0.0f);
		  };
		_panel.text = _burstMod.killCount + "/" + _burstMod.requireKillCount;
		_panel.SetBarScale(_burstMod.killCount / _burstMod.requireKillCount, 0.0f);
	}
}
