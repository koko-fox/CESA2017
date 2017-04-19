using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModeChanger : MonoBehaviour
{
	private HanachanCore _hanachanCore;
	[SerializeField]
	[Header("ハナちゃん側のカメラ")]
	private Camera _hanachanSideCamera;

	private SpecterCore _specterCore;
	[SerializeField]
	[Header("スペクター側のカメラ")]
	private Camera _specterSideCamera;

	private bool _isHanachanEnabled;
	public bool IsHanachanEnabled { get { return _isHanachanEnabled; } }
	private bool _isSpecterEnabled;
	public bool IsSpecterEnabled { get { return _isSpecterEnabled; } }

	private void Awake()
	{
		_hanachanCore = FindObjectOfType<HanachanCore>();
		_specterCore = FindObjectOfType<SpecterCore>();
		_isHanachanEnabled = true;
		_isSpecterEnabled = false;

		_specterCore.MovementModule.enabled = false;
		_specterCore.CameraControlModule.enabled = false;
		_specterCore.ConstructorModule.enabled = false;
		_specterSideCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.U))
		{
			if (_isHanachanEnabled)
			{
				_hanachanCore.ShieldControlModule.enabled = false;
				_hanachanCore.MovementModule.enabled = false;
				_hanachanSideCamera.enabled = false;

				_specterCore.CameraControlModule.enabled = true;
				_specterCore.MovementModule.enabled = true;
				_specterCore.ConstructorModule.enabled = true;
				_specterSideCamera.enabled = true;

				_isHanachanEnabled = false;
				_isSpecterEnabled = true;
			}
			else
			{
				_hanachanCore.ShieldControlModule.enabled = true;
				_hanachanCore.MovementModule.enabled = true;
				_hanachanSideCamera.enabled = true;

				_specterCore.CameraControlModule.enabled = false;
				_specterCore.MovementModule.enabled = false;
				_specterCore.ConstructorModule.enabled = false;
				_specterSideCamera.enabled = false;

				_isHanachanEnabled = true;
				_isSpecterEnabled = false;
			}
		}
	}
}
