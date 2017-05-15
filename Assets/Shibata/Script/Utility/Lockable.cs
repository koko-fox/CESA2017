using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lockable : MonoBehaviour
{
	bool _isLock = false;
	/// <summary>
	/// ロックフラグ
	/// </summary>
	public bool isLock
	{
		get { return _isLock; }
		set { _isLock = value; }
	}

	protected virtual void LockableUpdate() { }
	protected virtual void LockableFixedUpdate() { }

	private void Update()
	{
		if (!isLock)
			LockableUpdate();
	}

	private void FixedUpdate()
	{
		if (!isLock)
			LockableFixedUpdate();
	}
}