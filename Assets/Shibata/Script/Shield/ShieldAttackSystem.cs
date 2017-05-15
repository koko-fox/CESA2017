using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttackSystem : Lockable
{
	public delegate float OnCalc();

	public event OnCalc onCalcValue = delegate{ return 1.0f; };

	[SerializeField]
	float _baseValue;
	/// <summary>
	/// ベースの攻撃力
	/// </summary>
	public float baseValue { get { return _baseValue; } }

	/// <summary>
	/// 最終攻撃力
	/// </summary>
	public float lastValue
	{
		get
		{
			float last = _baseValue;
			last *= onCalcValue();
			return last;
		}
	}
}
