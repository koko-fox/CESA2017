using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
  private void OnCollisionEnter(Collision collision) {
    var otherObject = collision.rigidbody.gameObject;
    var enemyScript = otherObject.GetComponent<Enemy>();
    if (enemyScript == null) return;
    enemyScript.Killed();
  }
}
