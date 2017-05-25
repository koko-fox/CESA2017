using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModeChanger : Lockable
{
	[SerializeField]
	ChanCore _chan;
	[SerializeField]
	SpecterCore _specter;

	bool _isChan = true;
	public bool isChan { get { return _isChan; } }

	private void Awake()
	{
		if(!_chan)
			_chan = FindObjectOfType<ChanCore>();
		if(!_specter)
			_specter = FindObjectOfType<SpecterCore>();

		Change(true);
	}

	public void Change(bool isChan)
	{
		_isChan = isChan;

		if (_isChan)
		{
			_chan.UnlockAll();
			_specter.LockAll();
		}
		else
		{
			_chan.LockAll();
			_specter.UnlockAll();
		}
	}

	protected override void LockableUpdate()
	{
		if(Input.GetKeyDown(KeyCode.U))
		{
			_isChan = !_isChan;

			if(_isChan)
			{
				_chan.UnlockAll();
				_specter.LockAll();
			}
			else
			{
				_chan.LockAll();
				_specter.UnlockAll();
			}
		}
	}
}
