using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanelLinker : MonoBehaviour
{
	#region inputs
	HUDTextPanelAccessor _panel;
	ChanFacade _facade;
	#endregion

	#region unity methods
	private void Awake()
	{
		_panel = GetComponent<HUDTextPanelAccessor>();
		_facade = FindObjectOfType<ChanFacadeHolder>().facade;
	}

	private void Start()
	{
		_facade.growthMod.onLevelChanged += () =>
		  {
			  _panel.str = "Lv." + _facade.growthMod.level;
		  };
		_panel.str = "Lv." + _facade.growthMod.level;
	}
	#endregion
}