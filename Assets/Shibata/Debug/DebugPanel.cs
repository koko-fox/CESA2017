using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
	static Vector2 _hideout = new Vector2(100000.0f, 100000.0f);

	#region properties
	Text _name;
	Text _text;
	Image _textBack;
	Camera _camera;
	Canvas _debugCanvas;
	RectTransform _rectTransform;
	LineRenderer _lineRenderer;
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
	#endregion
	private void Awake()
	{
		_text = transform.FindChild("Text").GetComponent<Text>();
		_name = transform.FindChild("Name").GetComponent<Text>();
		_textBack = transform.FindChild("TextBack").GetComponent<Image>();
		_rectTransform = GetComponent<RectTransform>();
		_lineRenderer = GetComponent<LineRenderer>();

		_debugCanvas = GameObject.Find("DebugCanvas").GetComponent<Canvas>();
		transform.SetParent(_debugCanvas.transform, false);
	}

	private void Start()
	{
		_name.text = "<color=#ffffff>" + _owner.name + "</color>";
	}

	private void Update()
	{
		var pos = Camera.main.WorldToScreenPoint(_owner.transform.position + _owner.transform.TransformDirection(_offset));
		var originPos = Camera.main.WorldToScreenPoint(_owner.transform.position);

		if (pos.z < 0.0f)
			_rectTransform.position = _hideout;
		else
			_rectTransform.position = pos;

		_lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(transform.position));
		_lineRenderer.SetPosition(1, _owner.transform.position);
	}

	void FixedUpdate()
	{
	}
}
