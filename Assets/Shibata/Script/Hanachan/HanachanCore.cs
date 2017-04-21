using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HanachanCore : MonoBehaviour
{
	public delegate void OnValueChanged();

	public event OnValueChanged OnHealthChanged = delegate { };
	public event OnValueChanged OnEnergyChanged = delegate { };

	[SerializeField]
	private float _maxHealth;
	public float MaxHealth { get { return _maxHealth; } }

	[SerializeField]
	private float _maxEnergy;
	public float MaxEnergy { get { return _maxEnergy; } }

	[SerializeField]
	private float _healthRegenAmount;

	[SerializeField]
	private float _energyRegenAmount;

	private float _health;
	public float Health
	{
		get { return _health; }
		set
		{
			_health = Mathf.Clamp(value, 0.0f, _maxHealth);
			OnHealthChanged();
		}
	}

	private float _energy;
	public float Energy
	{
		get { return _energy; }
		set
		{
			_energy = Mathf.Clamp(value, 0.0f, _maxEnergy);
			OnEnergyChanged();
		}
	}

	private HanachanMovementModule _movementModule;
	public HanachanMovementModule MovementModule { get { return _movementModule; } }

	private HanachanShieldControlModule _shieldControlModule;
	public HanachanShieldControlModule ShieldControlModule { get { return _shieldControlModule; } }

	private HanachanStatuses _statuses;
	public HanachanStatuses Statuses { get { return _statuses; } }

	private void Awake()
	{
		_movementModule = GetComponent<HanachanMovementModule>();
		_shieldControlModule = GetComponent<HanachanShieldControlModule>();
		_statuses = GetComponent<HanachanStatuses>();
	}

	private void Start()
	{
		_health = MaxHealth;
		_energy = _maxEnergy;
	}

	private void Update()
	{
		Health += _healthRegenAmount * Time.deltaTime;

		if(!_shieldControlModule.IsHold)
			Energy += _energyRegenAmount * Time.deltaTime;
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(HanachanCore))]
	[CanEditMultipleObjects]
	public class HanachanCoreEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			var param = target as HanachanCore;

			var maxHealth = EditorGUILayout.FloatField("最大ヘルス", param._maxHealth);
			var maxEnergy = EditorGUILayout.FloatField("最大EN", param._maxEnergy);

			var healthRegenAmount = EditorGUILayout.FloatField("ヘルス自然回復量(X/秒)", param._healthRegenAmount);
			var energyRegenAmount = EditorGUILayout.FloatField("EN自然回復量(X/秒)", param._energyRegenAmount);

			Undo.RecordObject(param, "HanachanCoreEditor Changed");

			param._maxHealth = maxHealth;
			param._maxEnergy = maxEnergy;
			param._healthRegenAmount = healthRegenAmount;
			param._energyRegenAmount = energyRegenAmount;

			EditorUtility.SetDirty(param);
		}
	}
#endif
}
