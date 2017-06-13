using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Chan/ChanBurstMod")]
public class ChanBurstMod : Module
{
	#region events
	public delegate void ValueChangedEventHandler();
	public event ValueChangedEventHandler onChangedKillCount = delegate { };
	#endregion

	#region inputs
	EnemyProvider _provider;
	ChanFacade _facade;
	#endregion

	#region private fields
	[SerializeField]
	int _requireKillCount = 50;
	[SerializeField]
	float _duration = 10.0f;
	float _remain;
	[SerializeField]
	float _speedMultiplier = 1.5f;
	const string _operandName = "[in burst]";
	bool _inBurst;
	int _killCount;
	#endregion

	#region properties
	/// <summary>バースト状態残り時間</summary>
	public float remain { get { return _remain; } }
	/// <summary>バースト状態か</summary>
	public bool inBurst { get { return _inBurst; } }
	/// <summary>バースト状態移行のために必要なキル数</summary>
	public int requireKillCount { get { return _requireKillCount; } }
	/// <summary>キルカウント</summary>
	public int killCount { get { return _killCount; } }
	#endregion

	#region private methods
	void BeginBurst()
	{
		EndBurst();
		_inBurst = true;
		_remain = _duration;
		_facade.movementMod.AddSpeedMultiplier(_operandName, _speedMultiplier);
	}
	void EndBurst()
	{
		_inBurst = false;
		_killCount = 0;
		onChangedKillCount();
		_facade.movementMod.RemoveSpeedMultiplier(_operandName);
	}
	#endregion

	#region public methods
	public void ForcedBurst()
	{
		BeginBurst();
	}
	#endregion

	#region override methods
	protected override void ModuleAwake()
	{
		if (!_provider)
			_provider = FindObjectOfType<EnemyProvider>();
	}

	public override void OrdableStart()
	{
		_facade = GetComponent<ChanFacadeHolder>().facade;

		if (!_provider)
			return;
		_provider.onDead += _provider_onDead;
	}

	public override void OrdableUpdate()
	{
		if(inBurst)
		{
			_remain -= Time.deltaTime;
			if (_remain <= 0.0f)
				EndBurst();
		}
	}
	#endregion

	#region event callbacks
	private void _provider_onDead(EnemyCore.DeathInfo info)
	{
		if (inBurst || info.Factor == EnemyCore.DeathInfo.DeathFactor.Suicided) 
			return;

		_killCount++;
		onChangedKillCount();
		if (_killCount >= _requireKillCount)
			BeginBurst();
	}
	#endregion
}
