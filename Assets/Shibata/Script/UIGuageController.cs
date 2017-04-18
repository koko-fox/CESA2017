using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGuageController : MonoBehaviour
{
	[SerializeField]
	private Image _guage;

	[SerializeField]
	private Image _guageBack;

	[SerializeField]
	private float _length;

	private float _rate = 1.0f;

	public void Reboot()
	{
		_guage.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guageBack.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guage.rectTransform.DOScaleX(_length * _rate, 1.0f);
		_guageBack.rectTransform.DOScaleX(_length, 0.2f);
	}

	private void Awake()
	{
		_guage.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guageBack.rectTransform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
		_guage.rectTransform.DOScaleX(_length, 1.0f);
		_guageBack.rectTransform.DOScaleX(_length, 0.2f);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.M))
			Reboot();
	}
}
