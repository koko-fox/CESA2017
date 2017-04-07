using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
  [SerializeField]
  private NavMeshAgent agent;
  [SerializeField]
  private GameObject target;
  [SerializeField]
  private Stage stage;

  public Stage Stage {
    get {
      return stage;
    }

    set {
      stage = value;
    }
  }

  private void Update() {
    if (target != null) {
      agent.destination = target.transform.position;
    }
    if (Input.GetKeyDown(KeyCode.I)) {
      Killed();
    }
  }

  private void OnTriggerStay(Collider other) {
    if (target != null) return;
    var otherObject = other.gameObject;
    if (otherObject.name == "Player") {
      target = otherObject;
    }
  }

  public void Killed() {
    StartCoroutine("Die");
  }

  private IEnumerator Die() {
    Vector3 scaleOrigin = transform.localScale;
    float t = 0.0f;
    while (transform.localScale.x > 0) {
      yield return null;
      t += Time.deltaTime / 0.2f;
      var scale = Vector3.MoveTowards(scaleOrigin, Vector3.zero, t);
      scale.y = scaleOrigin.y;
      transform.localScale = scale;
    }
    stage.TrySpawnEnemy();
    Destroy(gameObject);
  }

}
