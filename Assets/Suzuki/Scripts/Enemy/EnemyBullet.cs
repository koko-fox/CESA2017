using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
  [SerializeField]
  private float speed;
  [SerializeField]
  private float duration;
  [SerializeField]
  private float power;

  void Start() {
    var rigidbody = GetComponent<Rigidbody>();
    rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);

    StartCoroutine(Die());
  }

  private IEnumerator Die() {
    yield return new WaitForSeconds(duration);
    Destroy(gameObject);
  }

  private void OnCollisionEnter(Collision collision) {
    if (IsShield(collision.gameObject)) {
      var shield = collision.gameObject.GetComponent<ShieldCore>();
      shield.responseSystem.NoticeHitBullet(collision.contacts[0].point);
    }
    if (IsPlayer(collision.gameObject)) {
      var player = collision.gameObject.GetComponent<ChanResponseMod>();
      var hitpos = collision.contacts[0].point;
      player.NoticeHitBullet(power, hitpos);
    }
    Destroy(gameObject);
  }

  private bool IsPlayer(GameObject other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) return true;
    return false;
  }

  private bool IsShield(GameObject other) {
    var layer = LayerMask.NameToLayer("RadiateShield");
    if (other.gameObject.layer == layer) return true;
    return false;
  }
}
