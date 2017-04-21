using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HanachanStatuses : MonoBehaviour
{
	public delegate void OnValueChanged();

	public event OnValueChanged OnHealthChanged = delegate { };
	public event OnValueChanged OnEnergyChanged = delegate { };
	public event OnValueChanged OnArmorChanged = delegate { };
	public event OnValueChanged OnSpecialChanged = delegate { };
	public event OnValueChanged OnExpChanged = delegate { };

	[Header("移動系パラメータ設定")]
	[SerializeField]
	[Tooltip("移動速度")]
	private float _forwardSpeed;
	public float ForwardSpeed { get { return _forwardSpeed; } }

	[SerializeField]
	[Tooltip("後退速度")]
	private float _backSpeed;
	public float BackSpeed { get { return _backSpeed; } }

	[SerializeField]
	[Tooltip("横歩き速度")]
	private float _sideWalkSpeed;
	public float SideWalkSpeed { get { return _sideWalkSpeed; } }

	[SerializeField]
	private float _speedMagByDash;
	public float SpeedMagByDash { get { return _speedMagByDash; } }

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


#if UNITY_EDITOR

	[CustomEditor(typeof(HanachanStatuses))]
	public class HanachanStatusesEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			/*
			HanachanStatuses status = target as HanachanStatuses;

			status._forwardSpeed = EditorGUILayout.FloatField("前進速度", status._forwardSpeed);
			status._sideWalkSpeed = EditorGUILayout.FloatField("横歩き速度", status._sideWalkSpeed);
			status._backSpeed = EditorGUILayout.FloatField("後退速度", status._backSpeed);
			status._speedMagByDash = EditorGUILayout.FloatField("ダッシュ速度倍率", status._speedMagByDash);

			EditorGUILayout.Space();

			status._maxHealth = EditorGUILayout.FloatField("最大ヘルス", status._maxHealth);
			status._maxEnergy = EditorGUILayout.FloatField("最大エネルギー", status._maxEnergy);

			EditorGUILayout.Space();

			status._energyRegenRate = EditorGUILayout.FloatField("EN自然回復量(X/秒)", status._energyRegenRate);
			status._shieldShotCost = EditorGUILayout.FloatField("シールド発射時の消費EN", status._shieldShotCost);
			status._shieldHoldCost = EditorGUILayout.FloatField("シールド保持時の消費EN(X/秒)", status._shieldHoldCost);
			*/

			HanachanStatuses stat = target as HanachanStatuses;

			var forwardSpeed = EditorGUILayout.FloatField("前進速度", stat._forwardSpeed);
			var sideWalkSpeed = EditorGUILayout.FloatField("横歩き速度",
				stat._sideWalkSpeed);
			var backSpeed = EditorGUILayout.FloatField("後退速度",
				stat._backSpeed);
			var speedMagByDash = EditorGUILayout.FloatField("ダッシュ倍率",
				stat._speedMagByDash);

			var maxHealth = EditorGUILayout.FloatField("最大ヘルス",
				stat._maxHealth);
			var maxEnergy = EditorGUILayout.FloatField("最大エネルギー",
				stat._maxEnergy);

			var energyRegenRate = EditorGUILayout.FloatField("EN自然回復量(X/秒)", stat._energyRegenRate);
			var shieldShotCost = EditorGUILayout.FloatField("シールド発射消費EN", stat._shieldShotCost);
			var shieldHoldCost = EditorGUILayout.FloatField("シールド保持時消費EN", stat._shieldHoldCost);

			Undo.RecordObject(stat, "HanachanStatuses Changed");

			stat._forwardSpeed = forwardSpeed;
			stat._sideWalkSpeed = sideWalkSpeed;
			stat._backSpeed = backSpeed;
			stat._speedMagByDash = speedMagByDash;
			stat._maxHealth = maxHealth;
			stat._maxEnergy = maxEnergy;
			stat._energyRegenRate = energyRegenRate;
			stat._shieldShotCost = shieldShotCost;
			stat._shieldHoldCost = shieldHoldCost;

			EditorUtility.SetDirty(stat);
		}
	}

#endif
}