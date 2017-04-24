using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadShieldDestroySystem : MonoBehaviour
{
	[SerializeField]
	private float _lifeTime;
	public float LifeTime { get { return _lifeTime; } set { _lifeTime = value; } }

	private float _elapsedTime = 0.0f;

	private void Update()
	{
		_elapsedTime += Time.deltaTime;
		if (_elapsedTime >= _lifeTime)
			Destroy(transform.gameObject);
	}
}
