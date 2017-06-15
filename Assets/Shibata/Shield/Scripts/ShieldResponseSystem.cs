using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Shield/ShieldResponseSystem")]
[RequireComponent(typeof(AudioSource))]
public class ShieldResponseSystem : Lockable
{
	[SerializeField]
	AudioClip _hitSound=null;
	AudioSource _audioSource;

	[SerializeField]
	GameObject _hitParticle=null;

	private void Awake()
	{
		_audioSource = gameObject.GetComponent<AudioSource>();
		_audioSource.clip = _hitSound;
	}

	/// <summary>
	/// 弾が衝突したことを通知
	/// </summary>
	/// <param name="hitPos">衝突した位置</param>
	public void NoticeHitBullet(Vector3 hitPos)
	{
		if(_hitSound)
			AudioSource.PlayClipAtPoint(_hitSound, transform.position);

		if (_hitParticle)
		{
			var particle = Instantiate(_hitParticle);
			particle.transform.position = hitPos;
			particle.transform.rotation = Quaternion.LookRotation(transform.forward);
		}
	}
}