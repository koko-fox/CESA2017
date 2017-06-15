using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanResponseMod : Module
{
	#region inputs
	ChanHealthMod _healthMod;
	#endregion

	#region private fields
	[SerializeField]
	AudioClip _hitSound = null;
	#endregion

	#region public methods
	public void NoticeHitBullet(float damage,Vector3 hitPos)
	{
		if(!_hitSound)
		{
			return;
		}

		_healthMod.health -= damage;
		AudioSource.PlayClipAtPoint(_hitSound, hitPos);
	}
	#endregion

	#region override methods
	protected override void ModuleAwake()
	{
		_healthMod = GetComponent<ChanHealthMod>();
	}
	#endregion
}
