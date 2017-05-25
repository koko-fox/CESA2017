using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class CursorOperationModeChanger : Lockable
{
	bool _isFree = false;

	[SerializeField]
	ChanCore _chan;
	[SerializeField]
	SpecterCore _specter;
	[SerializeField]
	ControlModeChanger _controlModeChanger;

	private void Awake()
	{
		if(!_chan)
			_chan = FindObjectOfType<ChanCore>();
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

				_chan.LockAll();
				_specter.LockAll();
				_controlModeChanger.isLock = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				_chan.UnlockAll();
				_specter.UnlockAll();

				_controlModeChanger.isLock = false;
				_controlModeChanger.Change(_controlModeChanger.isChan);
			}
		}
	}
}
