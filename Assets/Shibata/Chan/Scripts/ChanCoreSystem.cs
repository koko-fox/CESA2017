using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ChanCoreSystem : Core
{
	DebugPanel[] _panel;
	StringBuilder _buffer;

	ChanFacade _facade;

	void Awake()
	{
		_panel = new DebugPanel[2];
		_panel[0] = DebugPanelManager.instance.Create(gameObject);
		_panel[0].offset = new Vector3(0.0f, 1.4f);
		_panel[0].fontSize = 7;

		_panel[1] = DebugPanelManager.instance.Create(gameObject);
		_panel[1].offset = new Vector3(-1.0f, 1.4f);
		_panel[1].fontSize = 7;

		_buffer = new StringBuilder();
	}

	protected override void Start()
	{
		_facade = GetComponent<ChanFacadeHolder>().facade;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		_buffer = new StringBuilder();
		_buffer.Append("applied ").Append(_modules.Count).Append(" modules\n");

		foreach(var elem in _modules)
		{
			_buffer.Append(elem.GetType().ToString().Remove(0,4)).Append(":");
			if (elem.locked)
				_buffer.Append("<color=#ff0000>locked</color>\n");
			else
				_buffer.Append("<color=#00ff00>unlocked</color>\n");
		}

		_buffer.Append("------------------\n");
		_buffer.Append("shield control state:\n");
		if (_facade.shieldControlMod.state == ChanShieldControlMod.State.ShotReady)
			_buffer.Append("<color=#00ff00>shot ready</color>\n");
		else
			_buffer.Append("<color=#fffff0>hold</color>\n");

		_buffer.Append("------------------\n");
		_buffer.Append("speed multipliers\n");
		foreach(var elem in _facade.movementMod.speedMultipliers)
		{
			_buffer.Append(elem._name).Append(":x").Append(elem._operand).Append("\n");
		}

		_panel[0].text = _buffer.ToString();

		_buffer = new StringBuilder();
		_buffer.Append("combo count:").Append(_facade.growthMod.comboCount).Append("[sec]\n");
		_buffer.Append("combo discard elapsed:\n").Append(_facade.growthMod.comboDiscardElapsed).Append("[sec]\n");

		_buffer.Append("------------------\n");

		_buffer.Append("burst state:");
		if (_facade.burstMod.inBurst)
		{
			_buffer.Append("<color=#00ff00>in burst</color>\n");
			_buffer.Append("remain:<color=#ff0000").Append(_facade.burstMod.remain).Append("</color>\n");
		}
		else
		{
			_buffer.Append("<color=#ff0000>now charging</color>\n");
			_buffer.Append("require kill:").Append(_facade.burstMod.killCount).Append("/").Append(_facade.burstMod.requireKillCount).Append("\n");
		}
		_panel[1].text = _buffer.ToString();
	}
}
