using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
	Text _name;
	Text _text;
	Image _textBack;
	Camera _camera;

	Vector3 _offset;
	public Vector3 offset
	{
		get { return _offset; }
		set { _offset = value; }
	}

	public GameObject _owner;

	public string text
	{
		get { return _text.text; }
		set
		{
			_text.text = value;
		}
	}

	public int fontSize
	{
		get { return _text.fontSize; }
		set { _text.fontSize = value; }
	}

	private void Awake()
	{
		_text = transform.FindChild("Text").GetComponent<Text>();
		_name = transform.FindChild("Name").GetComponent<Text>();
		_textBack = transform.FindChild("TextBack").GetComponent<Image>();
		_camera = FindObjectOfType<Camera>();

		var canvas = GameObject.Find("DebugCanvas");
		transform.SetParent(canvas.transform);
	}

	private void Start()
	{
		_name.text = "<color=#ffffff>" + _owner.name + "</color>";
	}

	private void Update()
	{
		transform.position = _owner.transform.position + transform.TransformDirection(offset);
	}

	private void OnGUI()
	{
		transform.SetSiblingIndex((int)(Vector3.Distance(_camera.transform.position, transform.position) * 1000.0f));
	}
}
