using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HUDPanelAccessor : Lockable
{
	[SerializeField]
	Text _valueText;
	[SerializeField]
	Image _bar;

	[SerializeField]
	Color _valueTextColor;
	public Color valueTextColor
	{
		get { return _valueTextColor; }
		set
		{
			_valueText.color = value;
			_valueTextColor = value;
		}
	}

	[SerializeField]
	Color _barColor;
	public Color barColor
	{
		get { return _barColor; }
		set
		{
			_bar.color = value;
			_barColor = value;
		}
	}

	[SerializeField]
	bool _valueTextVisible;
	/// <summary>
	/// 値表示テキストの表示/非表示
	/// </summary>
	public bool valueTextVisible
	{
		get { return _valueTextVisible; }
		set
		{
			_valueText.color = _valueTextColor * Convert.ToInt32(value);
			_valueTextVisible = value;
		}
	}
	[SerializeField]
	bool _barVisible;
	/// <summary>
	/// バーの表示/非表示
	/// </summary>
	public bool barVisible
	{
		get { return _barVisible; }
		set
		{
			_bar.color = _barColor * Convert.ToInt32(value);
			_barVisible = value;
		}
	}

	float _value = 0.0f;
	float _endValue = 0.0f;
	float _duration;
	float _elapsed;
	

	/// <summary>
	/// 値を設定する
	/// </summary>
	/// <param name="endValue">設定する値</param>
	/// <param name="duration">アニメーション時間</param>
	public void SetValue(float endValue,float duration)
	{
		_value = _endValue;
		_endValue = endValue;
		_duration = duration;
		_elapsed = 0.0f;
	}

	/// <summary>
	/// バーのスケールを設定する
	/// </summary>
	/// <param name="scale">スケール。通常は0.0~1.0の値で設定してください</param>
	/// <param name="duration">アニメーション時間</param>
	public void SetBarScale(float scale,float duration)
	{
		_bar.transform.DOScaleX(scale, duration);
	}

	public string text
	{
		get { return _valueText.text; }
		set { _valueText.text = value; }
	}

	private void Reset()
	{
		Awake();
	}

	private void Awake()
	{
		_valueText = transform.FindChild("ValueText").GetComponent<Text>();
		_valueText.color = _valueTextColor;
		valueTextVisible = _valueTextVisible;
		
		_bar = transform.FindChild("Bar").GetComponent<Image>();
		_bar.color = _barColor;
		barVisible = _barVisible;
	}

	protected override void LockableUpdate()
	{
		/*
		if (_elapsed < _duration)
		{
			_elapsed += Time.deltaTime;
			float lerped = Mathf.Lerp(_value, _endValue, _elapsed / _duration);
			_valueText.text = Mathf.FloorToInt(lerped).ToString();
		}
		*/
		
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(HUDPanelAccessor))]
	[CanEditMultipleObjects]
	class ThisEditor : Editor
	{
		bool foldout;

		public override void OnInspectorGUI()
		{
			var param = target as HUDPanelAccessor;

			var valueTextVisible = EditorGUILayout.Toggle("ValueTextVisible", param.valueTextVisible);
			var barVisible = EditorGUILayout.Toggle("BarVisible", param.barVisible);
			var valueTextColor = EditorGUILayout.ColorField("ValueTextColor", param.valueTextColor);
			var barColor = EditorGUILayout.ColorField("BarColor", param.barColor);

			Undo.RecordObject(param, "HUDPanelAccessor.ThisEditor Changed");

			param.valueTextColor = valueTextColor;
			param.barColor = barColor;
			param.valueTextVisible = valueTextVisible;
			param.barVisible = barVisible;

			foldout = EditorGUILayout.Foldout(foldout, "show default");
			if (foldout)
				DrawDefaultInspector();

			EditorUtility.SetDirty(param);
		}
	}
#endif
}
