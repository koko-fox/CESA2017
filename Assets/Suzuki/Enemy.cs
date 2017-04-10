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
  [SerializeField]
  private Sensor detectionArea;
  [SerializeField]
  private Sensor firingArea;
  [SerializeField]
  private EnemyGun gun;

  private List<int> collisions = new List<int>();

  public Stage Stage {
    get {
      return stage;
    }

    set {
      stage = value;
    }
  }

  enum Mode {
    Patrol,
    Attack
  }

  Mode mode = Mode.Patrol;

  private void Start() {
    detectionArea.OnSensorEnter = OnPlayerInDetectionArea;
    firingArea.OnSensorEnter = OnPlayerEnterFiringArea;
    firingArea.OnSensorExit = OnPlayerExitFiringAre;
  }

  private void Update() {
    RotateTowards();
    MoveTowards();
    switch (mode) {
    case Mode.Patrol: {
        break;
      }
    case Mode.Attack: {
        if (RaycastTest()) {
          gun.Fire();
        }
        break;
      }
    }

    if (Input.GetKeyDown(KeyCode.I)) {
      Killed();
    }
  }

  private void MoveTowards() {
    if (target != null) {
      agent.SetDestination(target.transform.position);
    }
  }
  private void RotateTowards() {
    if (target != null) {
      Vector3 direction = (target.transform.position - transform.position).normalized;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      var rotationSpeed = agent.angularSpeed;
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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
    Destroy(GetComponent<Rigidbody>());
    Destroy(GetComponent<Collider>());
    Destroy(GetComponent<NavMeshAgent>());
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

  public void OnCollisionEnter(Collision collision) {
    if (collisions.Contains(collision.gameObject.layer)) return;
    collisions.Add(collision.gameObject.layer);
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    var wallLayer = LayerMask.NameToLayer("Wall");
    if (collision.gameObject.layer == radiateShieldLayer) {
      gameObject.layer = LayerMask.NameToLayer("BlownEnemy");
    }
    if (collisions.Contains(radiateShieldLayer) && collisions.Contains(wallLayer)) {
      Killed();
    }
  }

  public void OnCollisionExit(Collision collision) {
    collisions.Remove(collision.gameObject.layer);
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    if (collision.gameObject.layer == radiateShieldLayer) {
      gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
  }

  private void OnPlayerInDetectionArea(Collider other) {
    if (target != null) return;
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) {
      target = other.gameObject;
    }
  }

  private void OnPlayerEnterFiringArea(Collider other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) {
      target = other.gameObject;
      mode = Mode.Attack;
    }
  }

  private void OnPlayerExitFiringAre(Collider other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) {
      mode = Mode.Patrol;
    }
  }

  private bool RaycastTest() {
    if (target != null) {
      var origin = transform.position;
      var direction = transform.forward;
      RaycastHit hitInfo;
      var maxDistance = 1000.0f;
      if (Physics.Raycast(origin, direction, out hitInfo, maxDistance)) {
        var playerLayer = LayerMask.NameToLayer("UnityChan");
        if (hitInfo.transform.gameObject.layer == playerLayer) {
          return true;
        }
      }
    }
    return false;
  }
}
