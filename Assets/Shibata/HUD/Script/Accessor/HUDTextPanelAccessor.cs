using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDTextPanelAccessor : MonoBehaviour
{
	Text _text;

	public string str
	{
		get { return _text.text; }
		set { _text.text = value; }
	}

	private void Awake()
	{
		_text = transform.FindChild("Text").GetComponent<Text>();
	}
}
