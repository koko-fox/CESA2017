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
  [SerializeField]
  private float durationToChase;
  private float remainingTimeToChase = 0.0f;

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
    Idle,
    Chase,
    Approach,
    Attack
  }

  [SerializeField]
  Mode mode = Mode.Idle;

  private void Start() {
    detectionArea.OnSensorEnter = OnPlayerEnterDetectionArea;
    detectionArea.OnSensorExit = OnPlayerExitDetectionArea;
    firingArea.OnSensorEnter = OnPlayerEnterFiringArea;
    firingArea.OnSensorExit = OnPlayerExitFiringAre;
  }

  private void Update() {
    MoveTowards();
    switch (mode) {
    case Mode.Idle: {
        break;
      }
    case Mode.Chase: {
        remainingTimeToChase -= Time.deltaTime;
        if (remainingTimeToChase < 0.0f) {
          target = null;
          agent.Stop();
          agent.ResetPath();
          agent.Resume();
          mode = Mode.Idle;
        }
        break;
      }
    case Mode.Approach: {
        break;
      }
    case Mode.Attack: {
        if (RaycastTest()) {
          agent.updatePosition = false;
          gun.Fire();
        }
        else {
          agent.updatePosition = true;
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
      agent.nextPosition = transform.position;
      agent.SetDestination(target.transform.position);
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

  private void OnPlayerEnterDetectionArea(Collider other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) {
      target = other.gameObject;
      mode = Mode.Approach;
    }
  }

  private void OnPlayerExitDetectionArea(Collider other) {
    if (target == null) return;
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) {
      remainingTimeToChase = durationToChase;
      mode = Mode.Chase;
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
      agent.nextPosition = transform.position;
      agent.updatePosition = true;
      mode = Mode.Approach;
    }
  }

  private bool RaycastTest() {
    if (target != null) {
      var origin = transform.position;
      var direction = transform.forward;
      RaycastHit hitInfo;
      var maxDistance = 1000.0f;
      var layerMask = LayerMask.GetMask("Enemy");
      layerMask = ~layerMask;
      if (Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask)) {
        var playerLayer = LayerMask.NameToLayer("UnityChan");
        if (hitInfo.transform.gameObject.layer == playerLayer) {
          return true;
        }
      }
    }
    return false;
  }
}
