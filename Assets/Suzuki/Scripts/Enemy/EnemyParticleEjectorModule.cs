using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

[DisallowMultipleComponent]
public class EnemyParticleEjectorModule : EnemyModuleBase {
  [SerializeField]
  private GameObject particlePrefab;

  protected override void DoDied() {
    Instantiate(particlePrefab, transform.position, transform.rotation);
  }
}
