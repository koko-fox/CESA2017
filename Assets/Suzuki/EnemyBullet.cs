using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
  [SerializeField]
  private Rigidbody rigidbody;
  [SerializeField]
  private float duration;
  [SerializeField]
  private float moveSpeed;

  private void Start() {
    rigidbody.AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);
  }

  private void Update() {
    duration -= Time.deltaTime;
    if (duration < 0.0f) {
      Destroy(gameObject);
    }
  }
}
