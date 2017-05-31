using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowWorldObject : MonoBehaviour
{
	#region properties
	RectTransform _rectTransform = null;
	[SerializeField]
	Transform _target = null;
	public Transform target
	{
		get { return _target; }
		set { _target = value; }
	}
	[SerializeField]
	Canvas _canvas;
	public Canvas canvas
	{
		get { return _canvas;}
		set { _canvas = value; }
	}
	#endregion

	void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
	//	_canvas = GetComponent<Graphic>().canvas;
	}

	void Update()
	{
		var pos = Vector2.zero;
		var uiCamera = Camera.main;
		var worldCamera = Camera.main;
		var canvasRect = _canvas.GetComponent<RectTransform>();

		var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, _target.position);

		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out pos);
		_rectTransform.position = pos;
	}
}
