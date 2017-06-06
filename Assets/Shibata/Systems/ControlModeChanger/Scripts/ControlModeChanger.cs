using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlModeChanger : Lockable
{
	[SerializeField]
	ChanCoreSystem _chan;
	[SerializeField]
	SpecterCore _specter;

	bool _isChan = true;
	public bool isChan { get { return _isChan; } }

	private void Awake()
	{
		if(!_chan)
			_chan = FindObjectOfType<ChanCoreSystem>();
		if(!_specter)
			_specter = FindObjectOfType<SpecterCore>();

		Change(true);
	}

	public void Change(bool isChan)
	{
		_isChan = isChan;

		if (_isChan)
		{
			foreach (var elem in _chan.modules)
				elem.locked = false;

			_specter.LockAll();
		}
		else
		{
			foreach (var elem in _chan.modules)
				elem.locked = true;

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
				foreach (var elem in _chan.modules)
					elem.locked = false;

				_specter.LockAll();
			}
			else
			{
				foreach (var elem in _chan.modules)
					elem.locked = true;

				_specter.UnlockAll();
			}
		}
	}
}
