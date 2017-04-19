using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGuageController : MonoBehaviour
{
	[SerializeField]
	[Header("ゲージの画像")]
	private Image _guage;

	[SerializeField]
	[Header("ゲージ背景の画像")]
	private Image _guageBack;

	[SerializeField]
	[Header("ゲージの長さ")]
	private float _length=1.0f;

	[SerializeField]
	[Header("ゲージの変形速度")]
	private float _guageScaleDuration=1.0f;

	[SerializeField]
	[Header("ゲージ背景の変形速度")]
	private float _guageBackScaleDuration=0.2f;

	private float _rate = 1.0f;
	public float Rate
	{
		get { return _rate; }
		set
		{
			_rate = Mathf.Clamp(value, 0.0f, 1.0f);
			_guage.rectTransform.DOScaleX(_length * _rate, _guageScaleDuration);
		}
	}

	public void Reboot()
	{
		_guage.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guageBack.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guage.rectTransform.DOScaleX(_length * _rate, _guageScaleDuration);
		_guageBack.rectTransform.DOScaleX(_length, _guageBackScaleDuration);
	}

	private void Awake()
	{
		_guage.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guageBack.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guage.rectTransform.DOScaleX(_length, _guageScaleDuration);
		_guageBack.rectTransform.DOScaleX(_length, _guageBackScaleDuration);

		Reboot();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.M))
			Rate -= 0.1f;
	}
}
