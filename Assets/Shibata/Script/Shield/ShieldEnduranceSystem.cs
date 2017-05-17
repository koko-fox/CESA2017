using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnduranceSystem : Lockable
{
	public delegate void OnDestroy();
	public event OnDestroy onDestroy = delegate { };

	ShieldCore _core;

	[SerializeField]
	float _endurance;
	/// <summary>
	/// 耐久度
	/// </summary>
	public float endurance
	{
		get { return _endurance; }
		set
		{
			_endurance = value;
			if (_endurance <= 0)
			{
				onDestroy();
			//	Destroy(gameObject);
			}
		}
	}


}
