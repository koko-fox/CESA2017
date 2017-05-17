using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDebugMessagePanelAccessor : MonoBehaviour
{
	Text _text;

	string _message;
	public string message { get { return _message; }set { _message = value; } }

	private void Awake()
	{
		_text = GetComponentInChildren<Text>();
	}

	private void FixedUpdate()
	{
		_text.text = _message;
		_message = "";	
	}


}
