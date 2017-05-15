using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Shield/ShieldCore")]
public class ShieldCore : MonoBehaviour
{
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

	private void Awake()
	{
		_movementSystem = GetComponent<ShieldMovementSystem>();
		_destroySystem = GetComponent<ShieldDestroySystem>();
		_attackSystem = GetComponent<ShieldAttackSystem>();
		_reinforcementSystem = GetComponent<ShieldReinforcementSystem>();
		_responseSystem = GetComponent<ShieldResponseSystem>();
	}
}
