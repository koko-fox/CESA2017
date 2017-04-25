using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadShieldCore : MonoBehaviour
{
	public delegate float OnCalc();

	[SerializeField]
	private float _baseAttackPower;
	private float _realAttackPower;
	public event OnCalc OnCalcAttackPower = delegate { return 1; };
	public float AttackPower
	{
		get
		{
			_realAttackPower = _baseAttackPower;
			_realAttackPower *= OnCalcAttackPower();
			return _realAttackPower;
		}
	}

	[SerializeField]
	private float _endurance;

	[SerializeField]
	private AudioClip _hitSound;
	private AudioSource _audioSource;

	/// <summary>
	/// ヒットサウンド用オーディオソースを再生する
	/// </summary>
	public void PlayHitSound()
	{
		AudioSource.PlayClipAtPoint(_hitSound, transform.position);
	}

	private void Awake()
	{
		_audioSource = gameObject.GetComponent<AudioSource>();
		_audioSource.clip = _hitSound;
	}
}
