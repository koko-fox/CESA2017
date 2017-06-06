using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarLinker : Lockable
{
	#region inputs
	HUDPanelAccessor _panel;
	ChanGrowthMod _growthMod;
	#endregion

	private void Awake()
	{
		_panel = GetComponent<HUDPanelAccessor>();
		_growthMod = FindObjectOfType<ChanGrowthMod>();
	}

	void Start ()
	{
		_growthMod.onExpChanged += () =>
		  {
			  float left = _growthMod.exp - _growthMod.prevRequireExp;
			  float mem = _growthMod.requireExpForLevelUp - _growthMod.prevRequireExp;
			  if (left / mem < 0.0f)
				  return;

			  _panel.SetBarScale(left / mem, 0.0f);
		  };
		{
			float left = _growthMod.exp - _growthMod.prevRequireExp;
			float mem = _growthMod.requireExpForLevelUp - _growthMod.prevRequireExp;
			if (left / mem < 0.0f)
				return;

			_panel.SetBarScale(left / mem, 0.0f);
		}
	}
}
