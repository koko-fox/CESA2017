using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class CursorOperationModeChanger : Lockable
{
	bool _isFree = false;

	[SerializeField]
	ChanCoreSystem _chan;
	[SerializeField]
	SpecterCore _specter;
	[SerializeField]
	ControlModeChanger _controlModeChanger;

	private void Awake()
	{
		if(!_chan)
			_chan = FindObjectOfType<ChanCoreSystem>();
		if(!_specter)
			_specter = FindObjectOfType<SpecterCore>();
		if (!_controlModeChanger)
			_controlModeChanger = FindObjectOfType<ControlModeChanger>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	protected override void LockableUpdate()
	{
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			_isFree = !_isFree;

			if(_isFree)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;

				foreach (var elem in _chan.modules)
					elem.locked = true;
				_specter.LockAll();
				_controlModeChanger.isLock = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				foreach (var elem in _chan.modules)
					elem.locked = false;
				_specter.UnlockAll();

				_controlModeChanger.isLock = false;
				_controlModeChanger.Change(_controlModeChanger.isChan);
			}
		}
	}
}
