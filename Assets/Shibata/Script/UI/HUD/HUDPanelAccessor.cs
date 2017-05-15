using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDPanelAccessor : Lockable
{
	[SerializeField]
	Text _valueText;
	[SerializeField]
	Image _bar;

	float _value = 0.0f;
	float _endValue = 0.0f;
	float _duration;
	float _elapsed;

	public void SetValue(float endValue,float duration)
	{
		_value = _endValue;
		_endValue = endValue;
		_duration = duration;
		_elapsed = 0.0f;
	}

	public void SetBarScale(float scale,float duration)
	{
		_bar.transform.DOScaleX(scale, duration);
	}

	protected override void LockableUpdate()
	{
		if (_elapsed < _duration)
		{
			_elapsed += Time.deltaTime;
			float lerped = Mathf.Lerp(_value, _endValue, _elapsed / _duration);
			_valueText.text = Mathf.FloorToInt(lerped).ToString();
		}
		
	}
}
