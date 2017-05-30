using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetonator : EnemyModuleBase {
  private KamikazeEnemyController controller;

  protected override void DoAwake() {
    controller = GetComponent<KamikazeEnemyController>();
  }

  private void OnCollisionEnter(Collision collision) {
    if (IsEnemy(collision.gameObject)) return;
    if (!controller.isFlying) return;
    core.Die();
  }

  private bool IsEnemy(GameObject other) {
    var layer = LayerMask.NameToLayer("Enemy");
    if (other.gameObject.layer == layer) return true;
    return false;
  }

}
