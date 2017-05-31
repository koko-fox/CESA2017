using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[AddComponentMenu("Chan/ChanCore")]
public class ChanCore : MonoBehaviour
{
	public struct LockInfo
	{
		public bool locked;
		public string name;
	}

	List<Lockable> _systems = new List<Lockable>();

	ChanHealthSystem _healthSystem;
	/// <summary>
	/// ヘルスシステムへのアクセス
	/// </summary>
	public ChanHealthSystem healthSystem { get { return _healthSystem; } }

	ChanEnergySystem _energySystem;
	/// <summary>
	/// エネルギーシステムへのアクセス
	/// </summary>
	public ChanEnergySystem energySystem { get { return _energySystem; } }

	ChanMovementSystem _movementSystem;
	/// <summary>
	/// 移動システムへのアクセス
	/// </summary>
	public ChanMovementSystem movementSystem { get { return _movementSystem; } }

	ChanShieldSystem _shieldSystem;
	/// <summary>
	/// シールドシステムへのアクセス
	/// </summary>
	public ChanShieldSystem shieldSystem { get { return _shieldSystem; } }

	ChanCameraControlSystem _cameraControlSystem;
	/// <summary>
	/// カメラ操作システムへのアクセス
	/// </summary>
	public ChanCameraControlSystem cameraControlSystem { get { return _cameraControlSystem; } }

	ChanGrowthSystem _growthSystem;
	/// <summary>
	/// 成長システムへのアクセス
	/// </summary>
	public ChanGrowthSystem growthSystem { get { return _growthSystem; } }

	ChanBurstSystem _burstSystem;
	/// <summary>
	/// バーストシステムへのアクセス
	/// </summary>
	public ChanBurstSystem burstSystem { get { return _burstSystem; } }

	/// <summary>
	/// 全てのシステムをロックする
	/// </summary>
	public void LockAll()
	{
		foreach (var elem in _systems)
			elem.isLock = true;
	}

	/// <summary>
	/// 全てのシステムをアンロックする
	/// </summary>
	public void UnlockAll()
	{
		foreach (var elem in _systems)
			elem.isLock = false;
	}

	/// <summary>
	/// 全てのシステムのロック状況を取得する
	/// </summary>
	/// <returns></returns>
	public List<LockInfo> GetLockInfo()
	{
		var info = new List<LockInfo>();

		foreach(var elem in _systems)
		{
			LockInfo buf;
			buf.locked = elem.isLock;
			buf.name = elem.GetType().Name;
			info.Add(buf);
		}

		return info;
	}

	DebugPanel _debugPanel;

	private void Awake()
	{
		_healthSystem = GetComponent<ChanHealthSystem>();
		_energySystem = GetComponent<ChanEnergySystem>();
		_movementSystem = GetComponent<ChanMovementSystem>();
		_shieldSystem = GetComponent<ChanShieldSystem>();
		_cameraControlSystem = GetComponent<ChanCameraControlSystem>();
		_growthSystem = GetComponent<ChanGrowthSystem>();
		_burstSystem = GetComponent<ChanBurstSystem>();

		_systems.Add(_healthSystem);
		_systems.Add(_energySystem);
		_systems.Add(_movementSystem);
		_systems.Add(_shieldSystem);
		_systems.Add(_cameraControlSystem);
		_systems.Add(_growthSystem);
		_systems.Add(_burstSystem);

		_debugPanel = DebugPanelManager.instance.Create(gameObject);
		_debugPanel.offset = new Vector3(0.0f, 1.8f);
		_debugPanel.fontSize = 7;
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_burstSystem.ForcedBurst();
		}

		StringBuilder str = new StringBuilder();

		str.Append("BurstMode:");
		if (_burstSystem.isBurst)
		{
			str.Append("<color=#00ff00>Active</color>\n");
			str.Append("Remain:").Append(_burstSystem.remainTime.ToString()).Append("[sec]\n");
		}
		else
			str.Append("<color=#ff0000>Inactive</color>\n");

		str.Append("-speed_multiplier_operands-\n");
		foreach(var elem in _movementSystem.SpeedMultipliers)
		{
			str.Append(elem._name).Append(":").Append(elem._operand).Append("\n");
		}
		str.Append("---------------------------\n");
		str.Append(_growthSystem.exp).Append("exp\n");
		str.Append("next require:").Append(_growthSystem.nextRequireExp).Append("exp\n");
		str.Append(_growthSystem.level).Append("lv\n");
		str.Append(_burstSystem.killCount).Append("kill\n");

		_debugPanel.text = str.ToString();
	}
}