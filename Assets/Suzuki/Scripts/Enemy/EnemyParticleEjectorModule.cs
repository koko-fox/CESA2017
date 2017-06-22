using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

[DisallowMultipleComponent]
public class EnemyParticleEjectorModule : EnemyModuleBase {
  [SerializeField]
  private PlaygroundParticlesC damageParticle;
  [SerializeField]
  private int emitParticlesPerDamaged;

  protected override void DoStart() {
    core.onDamaged += (float value) => {
      var minVelocity = damageParticle.initialLocalVelocityMin;
      var maxVelocity = damageParticle.initialLocalVelocityMax;
      var position = transform.position + Vector3.up;
      damageParticle.Emit(emitParticlesPerDamaged, position, minVelocity, maxVelocity, Color.white);
    };
  }
}
