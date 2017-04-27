using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestroy : MonoBehaviour
{
	private ParticleSystem _system;

	private void Awake()
	{
		_system = GetComponent<ParticleSystem>();	
	}

	// Update is called once per frame
	void Update ()
	{
		if (!_system.IsAlive())
			Destroy(gameObject);
	}
}
