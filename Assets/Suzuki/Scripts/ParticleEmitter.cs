using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

public class ParticleEmitter : MonoBehaviour {
  private Stage stage;

  [SerializeField]
  private PlaygroundParticlesC enemyDiedParticle;

  private void Awake() {
    stage = FindObjectOfType<Stage>();
  }

  private void Start() {
    stage.onEnemySpawned += enemy => {
      enemy.onDied += EmitEnemyDiedParticle;
    };
  }

  private void EmitEnemyDiedParticle(EnemyCore.DeathInfo info) {
    var position = info.Enemy.transform.position + Vector3.up * 1.0f;
    enemyDiedParticle.Emit(position);
  }
}
