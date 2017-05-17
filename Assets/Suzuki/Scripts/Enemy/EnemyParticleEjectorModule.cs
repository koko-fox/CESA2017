﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

[DisallowMultipleComponent]
public class EnemyParticleEjectorModule : EnemyModuleBase {
  [SerializeField]
  private GameObject particlePrefab;
  [SerializeField]
  private PlaygroundParticlesC damageParticle;

  protected override void DoStart() {
    core.onDamaged += (float value) => {
      var minVelocity = damageParticle.initialLocalVelocityMin;
      var maxVelocity = damageParticle.initialLocalVelocityMax;
      damageParticle.Emit(4, transform.position, minVelocity, maxVelocity, Color.white);
    };
  }

  protected override void DoDied() {
    Instantiate(particlePrefab, transform.position, transform.rotation);
  }
}
