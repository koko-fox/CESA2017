using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetonator : EnemyModuleBase {
  private KamikazeEnemyController controller;

  [SerializeField]
  private float explosionRadius;
  [SerializeField]
  private float firePower;
  [SerializeField]
  private LayerMask targetLayer;

  protected override void DoAwake() {
    controller = GetComponent<KamikazeEnemyController>();
  }

  private void OnCollisionEnter(Collision collision) {
    if (IsEnemy(collision.gameObject)) return;
    if (!controller.IsFlying) return;

    // 爆風発生
    var position = transform.position;
    var radius = explosionRadius;
    var layerMask = targetLayer;

    Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask);
    foreach (var elem in colliders) {
      var layer = LayerMask.NameToLayer("RadiateShield");
      if (elem.gameObject.layer == layer) {
        var shield = elem.gameObject.GetComponent<ShieldCore>();
        shield.responseSystem.NoticeHitBullet(collision.contacts[0].point);
      }
      layer = LayerMask.NameToLayer("UnityChan");
      if (elem.gameObject.layer == layer) {
        var player = elem.gameObject.GetComponent<ChanHealthMod>();
        player.health -= firePower;
      }
    }

    var info = new EnemyCore.DeathInfo();
    info.Enemy = core;
    info.Factor = EnemyCore.DeathInfo.DeathFactor.Suicided;
    core.Die(info);
  }

  private bool IsEnemy(GameObject other) {
    var layer = LayerMask.NameToLayer("Enemy");
    if (other.gameObject.layer == layer) return true;
    return false;
  }

}
