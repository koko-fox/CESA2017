using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMessagePanel : MonoBehaviour
{
	[SerializeField]
	GameObject _target;

	RectTransform _rectTrans;
	Text _text;
	string _message;
	public string message
	{
		get { return _message; }
		set { _message = value; }
	}

	private void Awake()
	{
		_rectTrans = GetComponent<RectTransform>();
		_text = GetComponentInChildren<Text>();
	}

	private void Update()
	{
		_rectTrans.position = _target.transform.position;
	}

	private void FixedUpdate()
	{
		_text.text = _message;
		_message = "";
	}
}
