using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
	#region private fields
	static Vector2 _hideout = new Vector2(100000.0f, 100000.0f);
	Text _title;
	Text _text;
	Canvas _debugCanvas;
	RectTransform _rectTransform;
	LineRenderer _lineRenderer;
	Vector3 _offset;
	GameObject _owner;
	bool _moveFixed;
	#endregion

	#region properties
	public string title
	{
		get { return _title.text; }
		set { _title.text = value; }
	}
	public string text
	{
		get { return _text.text; }
		set { _text.text = value; }
	}
	public Vector3 offset
	{
		get { return _offset; }
		set { _offset = value; }
	}
	public int fontSize
	{
		get { return _text.fontSize; }
		set { _text.fontSize = value; }
	}
	public GameObject owner
	{
		get { return _owner; }
		set { _owner = value; }
	}
	public bool moveFixed
	{
		get { return _moveFixed; }
		set { _moveFixed = value; }
	}
	#endregion
	private void Awake()
	{
		_text = transform.FindChild("Text").GetComponent<Text>();
		_title = transform.FindChild("Name").GetComponent<Text>();
		_rectTransform = GetComponent<RectTransform>();
		_lineRenderer = GetComponent<LineRenderer>();

		_debugCanvas = GameObject.Find("DebugCanvas").GetComponent<Canvas>();
		transform.SetParent(_debugCanvas.transform, false);
	}

	private void Start()
	{
		var pos = Camera.main.WorldToScreenPoint(_owner.transform.position + _owner.transform.TransformDirection(_offset));
		var originPos = Camera.main.WorldToScreenPoint(_owner.transform.position);
		if (pos.z < 0.0f)
			_rectTransform.position = _hideout;
		else
			_rectTransform.position = pos;
	}

	private void Update()
	{
		if (!_moveFixed)
		{
			var pos = Camera.main.WorldToScreenPoint(_owner.transform.position + _owner.transform.TransformDirection(_offset));
			var originPos = Camera.main.WorldToScreenPoint(_owner.transform.position);

			if (pos.z < 0.0f)
				_rectTransform.position = _hideout;
			else
				_rectTransform.position = pos;
		}

		_lineRenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(transform.position));
		_lineRenderer.SetPosition(1, _owner.transform.position);
	}
}
