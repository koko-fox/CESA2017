using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
  public Vector3 direction { get; set; }
  [SerializeField]
  private float speed;

  void Start() {
    StartCoroutine(Die());
  }

  private void FixedUpdate() {
    transform.position += direction * speed * Time.fixedDeltaTime;
  }

  private IEnumerator Die() {
    yield return new WaitForSeconds(1.0f);
    Destroy(gameObject);
  }

}
