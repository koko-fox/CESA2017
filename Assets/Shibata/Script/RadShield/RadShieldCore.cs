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

	[SerializeField]
	private GameObject _hitParticle;

	/// <summary>
	/// ヒットサウンド用オーディオソースを再生する
	/// </summary>
	public void PlayHitSound()
	{
		AudioSource.PlayClipAtPoint(_hitSound, transform.position);
	}


	/// <summary>
	/// 弾が当たったことを通知
	/// </summary>
	/// <param name="hitPos">衝突した位置</param>
	public void NoticeHitBullet(Vector3 hitPos)
	{
		AudioSource.PlayClipAtPoint(_hitSound, transform.position);

		var particle = Instantiate(_hitParticle);
		particle.transform.position = hitPos;
		particle.transform.rotation = Quaternion.LookRotation(transform.forward);
	}

	private void Awake()
	{
		_audioSource = gameObject.GetComponent<AudioSource>();
		_audioSource.clip = _hitSound;
	}

	private void FixedUpdate()
	{
		DebugTextWriter.Write("atk:"+AttackPower.ToString());
	}
}
