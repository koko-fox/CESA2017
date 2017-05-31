using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanHealthMod")]
public class ChanHealthMod : Module
{
	#region events
	public delegate void HealthEventHandler();
	/// <summary>ヘルスが変更されたとき</summary>
	public event HealthEventHandler onHealthChanged = delegate { };
	#endregion

	#region properties
	[SerializeField]
	float _maxHealth;
	/// <summary>最大ヘルス</summary>
	public float maxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = Mathf.Clamp(value, 0.0f, value); }
	}

	float _health;
	/// <summary>ヘルス</summary>
	public float health
	{
		get { return _health; }
		set
		{
			_health = Mathf.Clamp(value, 0.0f, _maxHealth);
			onHealthChanged();
		}
	}

	[SerializeField]
	private float _regenSpeed;
	/// <summary>ヘルス回復速度</summary>
	public float regenSpeed
	{
		get { return _regenSpeed; }
		set { _regenSpeed = value; }
	}
	#endregion

	protected override void ModuleAwake()
	{
		health = maxHealth;
	}

	#region ordable methods
	public override void OrdableUpdate()
	{
		health += regenSpeed * Time.deltaTime;
	}
	#endregion
}
