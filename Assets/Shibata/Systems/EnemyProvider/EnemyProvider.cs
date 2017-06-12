using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProvider : MonoBehaviour
{
	public delegate void DeadEventHandler(EnemyCore.DeathInfo factor);
	/// <summary>
	/// enemyが死んだとき
	/// </summary>
	public event DeadEventHandler onDead = delegate { };

	[SerializeField]
	Stage _stage;

	private void Awake()
	{
		if (!_stage)
			_stage = FindObjectOfType<Stage>();
	}

	private void Start()
	{
		if (!_stage)
			return;

		_stage.onEnemySpawned += _stage_onEnemySpawned;
	}

	private void _stage_onEnemySpawned(EnemyCore enemy)
	{
		enemy.onDied += Enemy_onDied;
	}

	private void Enemy_onDied(EnemyCore.DeathInfo info)
	{
		onDead(info);
	}

	public void ThrowDummy()
	{
		EnemyCore.DeathInfo info=new EnemyCore.DeathInfo();
		info.Enemy = null;
		info.Factor = EnemyCore.DeathInfo.DeathFactor.KilledByPlayer;
		onDead(info);
	}
}
