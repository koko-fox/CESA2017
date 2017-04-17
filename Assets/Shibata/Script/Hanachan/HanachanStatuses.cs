using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanStatuses : MonoBehaviour
{
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

	//現在HP
	private float _health;
	public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0.0f, _maxHealth); } }
	//装甲値
	private float _armorValue;
	public float ArmorValue { get { return _armorValue; } set { _armorValue = value; } }
	//エネルギー値
	private float _energyValue;
	public float EnergyValue { get { return _energyValue; } set { _energyValue = Mathf.Clamp(value, 0.0f, _maxEnergy); } }
	//SP値
	private float _specialValue;
	public float SpecialValue { get { return _specialValue; } set { _specialValue = Mathf.Clamp(value, 0.0f, _maxSpecial); } }
	//取得経験値オーブの個数
	private float _expOrbNum;
	public float ExpOrbNum { get { return _expOrbNum; } set { _expOrbNum = value; } }
}
