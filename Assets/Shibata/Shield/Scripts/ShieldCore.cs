using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Shield/ShieldCore")]
public class ShieldCore : MonoBehaviour
{
	List<Lockable> _systems;

	ShieldMovementSystem _movementSystem;
	/// <summary>
	/// 移動システムへのアクセス
	/// </summary>
	public ShieldMovementSystem movementSystem { get { return _movementSystem; } }

	ShieldDestroySystem _destroySystem;
	/// <summary>
	/// 自壊システムへのアクセス
	/// </summary>
	public ShieldDestroySystem destroySystem { get { return _destroySystem; } }

	ShieldAttackSystem _attackSystem;
	/// <summary>
	/// 攻撃システムへのアクセス
	/// </summary>
	public ShieldAttackSystem attackSystem { get { return _attackSystem; } }

	ShieldReinforcementSystem _reinforcementSystem;
	/// <summary>
	/// 強化システムへのアクセス
	/// </summary>
	public ShieldReinforcementSystem reinforcementSystem { get { return reinforcementSystem; } }

	ShieldResponseSystem _responseSystem;
	/// <summary>
	/// レスポンスシステムへのアクセス
	/// </summary>
	public ShieldResponseSystem responseSystem { get { return _responseSystem; } }

	DebugPanel _debugPanel;

	private void Awake()
	{
		_movementSystem = GetComponent<ShieldMovementSystem>();
		_destroySystem = GetComponent<ShieldDestroySystem>();
		_attackSystem = GetComponent<ShieldAttackSystem>();
		_reinforcementSystem = GetComponent<ShieldReinforcementSystem>();
		_responseSystem = GetComponent<ShieldResponseSystem>();

		_systems = new List<Lockable>();
		_systems.Add(_movementSystem);
		_systems.Add(_destroySystem);
		_systems.Add(_attackSystem);
		_systems.Add(_reinforcementSystem);
		_systems.Add(_responseSystem);

		_debugPanel = DebugPanelManager.instance.Create(gameObject);
		_debugPanel.offset = new Vector3(1.0f, 1.0f);
		_debugPanel.fontSize = 7;
	}

	private void Update()
	{
		_debugPanel.text = "";
		foreach (var elem in _systems)
		{
			_debugPanel.text += elem.GetType().ToString() + ":";
			if (!elem.isLock)
				_debugPanel.text += "<color=#00ff00>Enable</color>";
			else
				_debugPanel.text += "<color=#ff00ff>Disable</color>";

			_debugPanel.text += "\n";
		}
	}

	private void OnDestroy()
	{
		Destroy(_debugPanel.gameObject);
	}
}
