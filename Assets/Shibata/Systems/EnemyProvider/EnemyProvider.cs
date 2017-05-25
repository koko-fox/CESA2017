using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProvider : MonoBehaviour
{
	public delegate void DeadEventHandler();
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

	private void Enemy_onDied()
	{
		onDead();
	}
}
