using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanBurstSystem : Lockable
{
	[SerializeField]
	EnemyProvider _provider;

	[SerializeField]
	int _requireKillCount;
	public int requireKillCount
	{
		get { return _requireKillCount; }
	}

	int _killCount = 0;
	public int killCount
	{
		get { return _killCount; }
	}

	private void Awake()
	{
		if (!_provider)
			_provider = FindObjectOfType<EnemyProvider>();
	}

	private void Start()
	{
		if (!_provider)
			return;

		_provider.onDead += _provider_onDead;
	}

	private void _provider_onDead()
	{
		_killCount++;
	}
}
