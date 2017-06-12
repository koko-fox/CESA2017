using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUDComboBonus : MonoBehaviour
{
	#region inputs
	[SerializeField]
	Image _barImage;
	[SerializeField]
	Text _comboText;
	[SerializeField]
	Text _bonusText;
	ChanGrowthMod _chan;
	#endregion

	void SetChildActive(bool value)
	{
		_barImage.gameObject.SetActive(value);
		_comboText.gameObject.SetActive(value);
		_bonusText.gameObject.SetActive(value);
	}

	void Awake()
	{
		_chan = FindObjectOfType<ChanGrowthMod>();
	}

	void Start()
	{
		_chan.onStartCombo += _chan_onStartCombo;
		_chan.onWhileCombo += _chan_onWhileCombo;
		_chan.onEndCombo += _chan_onEndCombo;

		SetChildActive(false);
	}

	private void _chan_onEndCombo()
	{
		SetChildActive(false);
	}

	private void _chan_onWhileCombo()
	{
		float rate = _chan.comboRemainTime / _chan.comboDiscardDuration;
		_barImage.transform.localScale = new Vector3(rate, 1f, 1f);

		_comboText.text = string.Format("{0} COMBO", _chan.comboCount);
		_bonusText.text = string.Format("+ {0} % exp bonus", Mathf.FloorToInt(_chan.expMultiplierFromCombo * 100f) - 100f);
	}

	private void _chan_onStartCombo()
	{
		SetChildActive(true);

		_barImage.transform.localScale = new Vector3(1f, 1f, 1f);
	}
}
