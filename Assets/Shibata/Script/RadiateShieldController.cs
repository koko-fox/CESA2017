using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiateShieldController : MonoBehaviour
{
	public enum Mode
	{
		Retension,	//保持
		Injection,	//射出
	}

	private Mode currentMode = Mode.Injection;
	public Mode CurrentMode
	{
		set
		{
			currentMode = value;
		}
		get
		{
			return currentMode;
		}
	}

	[SerializeField]
	[Header("発射する力")]
	private float _movementForce = 100.0f;

	[SerializeField]
	[Header("自然消滅までの時間")]
	private float _lifeTime = 3.0f;

	[SerializeField]
	[Header("ヒット時に再生するサウンド")]
	private AudioClip _hitSound;
	private AudioSource _audioSource;

	//経過時間
	private float _elapsedTime = 0.0f;
	//剛体
	private Rigidbody _rigidBody;

	/// <summary>
	/// ヒットサウンド用オーディオソースを再生する
	/// </summary>
	public void PlayHitSound()
	{
		AudioSource.PlayClipAtPoint(_hitSound, transform.position);
		//_audioSource.Play();
	}

	private void Awake()
	{
		_audioSource = gameObject.GetComponent<AudioSource>();
		_audioSource.clip = _hitSound;
	}

	// Use this for initialization
	void Start ()
	{
		_rigidBody = transform.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (currentMode == Mode.Injection)
		{
			_elapsedTime += Time.deltaTime;
			if (_elapsedTime >= _lifeTime)
				Destroy(transform.gameObject);

			_rigidBody.AddForce(transform.forward * _lifeTime, ForceMode.VelocityChange);
		}
	}
}
