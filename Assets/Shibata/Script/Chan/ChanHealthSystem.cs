using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanHealthSystem")]
/// <summary>
/// ヘルスの概念を追加するモジュール
/// </summary>
public class ChanHealthSystem : Lockable
{
	public delegate void OnValueChanged();
	public event OnValueChanged onHealthChanged = delegate { };

	[SerializeField]
	private float _maxHealth;
	/// <summary>
	/// 最大ヘルス
	/// </summary>
	public float MaxHealth
	{
		get { return _maxHealth; }
		set { _maxHealth = Mathf.Clamp(value, 0.0f, value); }
	}

	private bool _isZero = false;
	/// <summary>
	/// ヘルスがゼロ以下か
	/// </summary>
	public bool isZero { get { return _isZero; } }

	private float _health;
	/// <summary>
	/// ヘルスの現在値
	/// </summary>
	public float health
	{
		get { return _health; }
		set
		{
			_isZero = (_health - value) <= 0.0f;
			_health = Mathf.Clamp(value, 0.0f, _maxHealth);
			onHealthChanged();
		}
	}

	[SerializeField]
	private float _regenSpeed;
	/// <summary>
	/// 回復速度
	/// </summary>
	public float regenSpeed
	{
		get { return _regenSpeed; }
		set { _regenSpeed = value; }
	}

	private void Awake()
	{
		health = _maxHealth;
	}

	protected override void LockableUpdate()
	{
		health += regenSpeed * Time.deltaTime;
	}
}
