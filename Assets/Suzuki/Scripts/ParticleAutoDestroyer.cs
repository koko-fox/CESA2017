using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

public class ParticleAutoDestroyer : MonoBehaviour {
  private PlaygroundParticlesC particles;

  private void Awake() {
    particles = GetComponent<PlaygroundParticlesC>();
  }

  private void Update() {
    if (!particles.IsAlive()) {
      Destroy(gameObject);
    }
  }

}
