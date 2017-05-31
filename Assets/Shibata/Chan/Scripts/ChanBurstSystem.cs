using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanBurstSystem : Lockable
{
	[SerializeField]
	EnemyProvider _provider;

	ChanCore _core;

	[SerializeField]
	int _requireKillCount = 50;

	[SerializeField]
	float _duration = 10.0f;
	float _remainTime;


	const string _operandName = "InBurst";
	[SerializeField]
	float _speedMultiplier = 1.5f;

	/// <summary>
	/// バーストモード残り時間
	/// </summary>
	public float remainTime
	{
		get { return _remainTime; }
	}

	bool _isBurst;
	/// <summary>
	/// バースト状態か
	/// </summary>
	public bool isBurst
	{
		get { return _isBurst; }
	}

	/// <summary>
	/// バーストモード移行のために必要なキル数
	/// </summary>
	public int requireKillCount
	{
		get { return _requireKillCount; }
	}

	int _killCount = 0;
	/// <summary>
	/// キルカウント
	/// </summary>
	public int killCount
	{
		get { return _killCount; }
	}

	private void Awake()
	{
		if (!_provider)
			_provider = FindObjectOfType<EnemyProvider>();

		_core = GetComponent<ChanCore>();
	}

	private void Start()
	{
		if (!_provider)
			return;

		_provider.onDead += _provider_onDead;
	}

	private void Update()
	{
		if(_isBurst)
		{
			_remainTime -= Time.deltaTime;
			if (_remainTime <= 0.0f)
			{
				EndBurst();
			}
		}
	}

	private void _provider_onDead()
	{
		if (_isBurst)
			return;

		_killCount++;
		
		if(_killCount>=_requireKillCount)
		{
			BeginBusrt();
		}
	}

	/// <summary>
	/// バースト開始
	/// </summary>
	void BeginBusrt()
	{
		EndBurst();

		_isBurst = true;
		_remainTime = _duration;
		_core.movementSystem.AddSpeedMultiplier(_operandName, _speedMultiplier);
	}

	/// <summary>
	/// バースト終了
	/// </summary>
	void EndBurst()
	{
		_isBurst = false;
		_killCount = 0;
		_core.movementSystem.RemoveSpeedMultiplier(_operandName);
	}

	/// <summary>
	/// 強制的にバーストモードへ移行させる
	/// </summary>
	public void ForcedBurst()
	{
		BeginBusrt();
	}
}
