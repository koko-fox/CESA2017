using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

public class ChanCoreSystem : Core
{
	static public StringBuilder _buffer;

	ChanFacade _facade;

	DebugBoardRegister.Panel _panel;

	void Awake()
	{
		_panel = DebugBoardRegister.CreatePanel();
		_buffer = new StringBuilder();
	}

	protected override void Start()
	{
		_facade = GetComponent<ChanFacadeHolder>().facade;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		_panel.title = "chan(1)";

		_buffer = new StringBuilder();

		_buffer.Append("<color=#ffffff>");
		_buffer.Append("speed multipliers\n");
		foreach(var elem in _facade.movementMod.speedMultipliers)
		{
			_buffer.AppendFormat("{0}:x{1}\n", elem._name, elem._operand);
			//_buffer.Append(elem._name).Append(":x").Append(elem._operand).Append("\n");
		}

		_panel.text = _buffer.ToString();

		//_buffer = new StringBuilder();
		_buffer.AppendFormat("combo count:{0}[sec]\n", _facade.growthMod.comboCount);
		//_buffer.Append("combo count:").Append(_facade.growthMod.comboCount).Append("[sec]\n");
		_buffer.AppendFormat("combo discard elapsed:{0}[sec]\n", _facade.growthMod.comboDiscardElapsed);
		//_buffer.Append("combo discard elapsed:\n").Append(_facade.growthMod.comboDiscardElapsed).Append("[sec]\n");

		_buffer.Append("------------------\n");

		_buffer.Append("burst state:");
		if (_facade.burstMod.inBurst)
		{
			_buffer.Append("<color=#00ff00>in burst</color>\n");
			_buffer.AppendFormat("remain:<color=#ff0000>{0}</color>\n", _facade.burstMod.remain);
		}
		else
		{
			_buffer.Append("<color=#ff0000>now charging</color>\n");
			_buffer.AppendFormat("require kill:{0}/{1}\n", _facade.burstMod.killCount, _facade.burstMod.requireKillCount);
			//_buffer.Append("require kill:").Append(_facade.burstMod.killCount).Append("/").Append(_facade.burstMod.requireKillCount).Append("\n");
		}

		_buffer.Append(transform.position);
		_buffer.Append("</color>");
		_panel.text = _buffer.ToString();


	}
}
