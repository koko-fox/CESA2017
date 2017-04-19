using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanUILinkModule : MonoBehaviour
{
	private HanachanStatuses _statuses;

	[SerializeField]
	[Header("ヘルスゲージ操作スクリプト")]
	private UIGuageController _healthGuageController;

	[SerializeField]
	[Header("エネルギーゲージ操作スクリプト")]
	private UIGuageController _energyGuageController;

	private void Awake()
	{
		_statuses = GetComponent<HanachanStatuses>();

		_statuses.OnHealthChanged += () => { _healthGuageController.Rate = _statuses.Health / _statuses.MaxHealth; };
		_statuses.OnEnergyChanged += () => { _energyGuageController.Rate = _statuses.EnergyValue / _statuses.MaxEnergy; };
	}
}
