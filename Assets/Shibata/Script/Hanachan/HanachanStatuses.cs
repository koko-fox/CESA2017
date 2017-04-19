using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnHealthChanged();
public delegate void OnEnergyChanged();
public delegate void OnArmorChanged();
public delegate void OnSpecialChanged();
public delegate void OnExpChanged();

public class HanachanStatuses : MonoBehaviour
{
	private static void Null() { }

	public OnHealthChanged OnHealthChanged = Null;
	public OnEnergyChanged OnEnergyChanged = Null;
	public OnArmorChanged OnArmorChanged = Null;
	public OnSpecialChanged OnSpecialChanged = Null;
	public OnExpChanged OnExpChanged = Null;

	[Header("移動系パラメータ設定")]
	[SerializeField]
	[Tooltip("移動速度")]
	private float _forwardSpeed = 3.0f;
	public float ForwardSpeed { get { return _forwardSpeed; } }

	[SerializeField]
	[Tooltip("後退速度")]
	private float _backSpeed = 1.5f;
	public float BackSpeed { get { return _backSpeed; } }

	[SerializeField]
	[Tooltip("横歩き速度")]
	private float _sideWalkSpeed = 1.5f;
	public float SideWalkSpeed { get { return _sideWalkSpeed; } }

	[Header("ステータス系パラメータ設定")]
	[SerializeField]
	[Tooltip("HP最大値")]
	private float _maxHealth;
	public float MaxHealth { get { return _maxHealth; } }

	[SerializeField]
	[Tooltip("EN最大値")]
	private float _maxEnergy;
	public float MaxEnergy { get { return _maxEnergy; } }

	[SerializeField]
	[Tooltip("SP最大値")]
	private float _maxSpecial;
	public float MaxSpecial { get { return _maxSpecial; } }

	[SerializeField]
	[Tooltip("EN自然回復量(X/秒)")]
	private float _energyRegenRate;
	public float EnergyRegenRate { get { return _energyRegenRate; } }

	[SerializeField]
	[Tooltip("シールド発射コスト")]
	private float _shieldShotCost;
	public float ShieldShotCost { get { return _shieldShotCost; } }

	[SerializeField]
	[Tooltip("シールド保持コスト(X/秒)")]
	private float _shieldHoldCost;
	public float ShieldHoldCost { get { return _shieldHoldCost; } }

	private float _health;
	/// <summary>
	/// 現在ヘルス
	/// </summary>
	public float Health
	{
		get { return _health; }
		set
		{
			_health = Mathf.Clamp(value, 0.0f, _maxHealth);
			OnHealthChanged();
		}
	}

	private float _armorValue;
	/// <summary>
	/// アーマー値
	/// </summary>
	public float ArmorValue
	{
		get { return _armorValue; }
		set
		{
			_armorValue = value;
			OnArmorChanged();
		}
	}

	//エネルギー値
	private float _energyValue;
	/// <summary>
	/// 現在エネルギー
	/// </summary>
	public float EnergyValue
	{
		get { return _energyValue; }
		set
		{
			_energyValue = Mathf.Clamp(value, 0.0f, _maxEnergy);
			OnEnergyChanged();
		}
	}

	//SP値
	private float _specialValue;
	/// <summary>
	/// 現在SP
	/// </summary>
	public float SpecialValue
	{
		get { return _specialValue; }
		set
		{
			_specialValue = Mathf.Clamp(value, 0.0f, _maxSpecial);
			OnSpecialChanged();
		}
	}

	//取得経験値オーブの個数
	private float _expOrbNum;
	/// <summary>
	/// 現在EXP
	/// </summary>
	public float ExpOrbNum
	{
		get { return _expOrbNum; }
		set
		{
			_expOrbNum = value;
			OnExpChanged();
		}
	}

	private void Start()
	{
		Health = MaxHealth;
		EnergyValue = MaxEnergy;
	}
}
