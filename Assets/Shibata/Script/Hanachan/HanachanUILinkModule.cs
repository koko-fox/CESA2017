using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanachanUILinkModule : MonoBehaviour
{
	private HanachanCore _core;

	[SerializeField]
	[Header("ヘルスゲージ操作スクリプト")]
	private UIGuageController _healthGuageController;

	[SerializeField]
	[Header("エネルギーゲージ操作スクリプト")]
	private UIGuageController _energyGuageController;

	private void Awake()
	{
		_core = GetComponent<HanachanCore>();

		_core.OnHealthChanged += () => { _healthGuageController.Rate = _core.Health / _core.MaxHealth; };
		_core.OnEnergyChanged += () => { _energyGuageController.Rate = _core.Energy / _core.MaxEnergy; };
	}
}
