using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEjector : MonoBehaviour
{
	[SerializeField]
	private GameObject _onDestroy;

	private void OnDestroy()
	{
		var particle = Instantiate(_onDestroy);
		particle.transform.position = transform.position;
	}
}
