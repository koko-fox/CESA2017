using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIGuageController : MonoBehaviour
{
	[SerializeField]
	private Image _guage;
	public Image Guage
	{
		get { return _guage; }
		set
		{
			_guage = value;
		}
	}

	[SerializeField]
	[Header("ゲージ背景の画像")]
	private Image _guageBack;

	[SerializeField]
	[Header("ゲージの長さ")]
	private float _length=1.0f;
	public float Length
	{
		get { return _length; }
		set
		{
			_length = value;
			_guage.rectTransform.localScale = new Vector3(_length, 1.0f, 1.0f);
			_guageBack.rectTransform.localScale = new Vector3(_length, 1.0f, 1.0f);
		}
	}

	[SerializeField]
	private Color _frontColor = Color.white;
	public Color FrontColor
	{
		get { return _frontColor; }
		set
		{
			_frontColor = value;
			_guage.color = _frontColor;
		}
	}

	[SerializeField]
	private Color _backColor = Color.white;
	public Color BackColor
	{
		get { return _backColor; }
		set
		{
			_backColor = value;
			_guageBack.color = _backColor;
		}
	}

	[SerializeField]
	private Color _iconColor = Color.white;
	public Color IconColor
	{
		get { return _iconColor; }
		set
		{
			_iconColor = value;
			
		}
	}


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
		_guage = transform.FindChild("Guage").GetComponent<Image>();
		_guageBack = transform.FindChild("GuageBack").GetComponent<Image>();

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

#if UNITY_EDITOR

	[CustomEditor(typeof(UIGuageController))]
	[CanEditMultipleObjects]
	public class UIGuageControllerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			UIGuageController cont = target as UIGuageController;

			var length = EditorGUILayout.FloatField("ゲージの長さ", cont.Length);
			var frontColor = EditorGUILayout.ColorField("フロントカラー", cont.FrontColor);
			var backColor = EditorGUILayout.ColorField("バックカラー", cont.BackColor);

			Undo.RecordObject(cont, "UIGuageController Changed");
			cont.Length = length;
			cont.FrontColor = frontColor;
			cont.BackColor = backColor;

			EditorUtility.SetDirty(cont);
		}
	}

#endif
}
