using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
  public delegate void OnEnemyKilledEvent(Enemy enemy);
  public OnEnemyKilledEvent OnKilled { get; set; }

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
  [SerializeField]
  private int moveSpeed;
  private float remainingTimeToChase = 0.0f;
  [SerializeField]
  private float fieldOfFire;
  [SerializeField]
  private float health;
  [SerializeField]
  private int firePower;
  [SerializeField]
  private GameObject[] dropItemPrefab;
  [SerializeField]
  private int[] dropRateTable;
  [SerializeField]
  private float rotationSpeed;
  [SerializeField]
  private AudioClip enemyKilledSound;

  private List<int> collisions = new List<int>();
  public Stage Stage {
    get {
      return stage;
    }

    set {
      stage = value;
    }
  }

  private bool isKilled = false;

  public
  enum Mode {
    Idle,
    Chase,
    Approach,
    Attack
  }

  [SerializeField]
  Mode mode = Mode.Idle;

  private void Start() {
    detectionArea.OnSensorEnter = OnTriggerEnterDetectionArea;
    detectionArea.OnSensorExit = OnTriggerExitDetectionArea;
    firingArea.OnSensorEnter = OnTriggerEnterFiringArea;
    firingArea.OnSensorExit = OnTriggerExitFiringArea;
    firingArea.OnSensorStay = OnTriggerStayFiringArea;
  }

  private void Update() {
    if (health <= 0) {
      if (!isKilled) {
        Killed();
        isKilled = true;
      }
    }
    else {
      MoveTowardTarget();
      RotationTowardTarget();
      switch (mode) {
        case Mode.Idle: break;
        case Mode.Chase:
          Chase();
          break;
        case Mode.Approach: break;
        case Mode.Attack:
          Attack();
          break;
      }
      Debug.DrawRay(transform.position, transform.forward * 20, Color.red);
    }
    if (Input.GetKeyDown(KeyCode.I)) {
      health = 0;
    }
  }

  private void RotationTowardTarget() {
    if (agent == null) return;
    if (target == null) return;
    var direction = (target.transform.position - transform.position).normalized;
    direction.y = 0.0f;
    var lookRotation = Quaternion.LookRotation(direction);
    var t = Time.deltaTime * rotationSpeed;
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, t);
  }

  private void MoveTowardTarget() {
    if (agent == null) return;
    if (target == null) return;
    agent.speed = moveSpeed / 10;
    agent.SetDestination(target.transform.position);
  }

  private void Attack() {
    if (!CanSeeTarget()) return;
    if (!IsTargetInFieldOfFire()) return;
    gun.Fire();
  }

  private void Chase() {
    remainingTimeToChase -= Time.deltaTime;
    if (remainingTimeToChase < 0.0f) {
      target = null;
      agent.ResetPath();
      mode = Mode.Idle;
    }
  }

  private bool CanSeeTarget() {
    if (target == null) return false;
    NavMeshHit hitInfo;
    if (agent.Raycast(target.transform.position, out hitInfo)) return false;
    return true;
  }

  private bool IsTargetInFieldOfFire() {
    if (target == null) return false;
    var heading = (target.transform.position - transform.position);
    var direction = heading.normalized;
    var dot = Vector3.Dot(transform.forward, direction);
    if (dot < fieldOfFire) return false;
    return true;
  }

  private void Killed() {
    Destroy(GetComponent<Rigidbody>());
    Destroy(GetComponent<Collider>());
    Destroy(GetComponent<NavMeshAgent>());
    Destroy(detectionArea);
    Destroy(firingArea);
    Destroy(gun);
    AudioSource.PlayClipAtPoint(enemyKilledSound, transform.position);

    StartCoroutine("Die");
  }

  private void DropItem() {
    for (int i = 0; i < dropRateTable.Length; ++i) {
      if (UnityEngine.Random.Range(0, 100) < dropRateTable[i]) {
        Instantiate(dropItemPrefab[i], transform.position, Quaternion.identity);
        return;
      }
    }
    Instantiate(dropItemPrefab[4], transform.position, Quaternion.identity);
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
    DropItem();
    OnKilled(this);
    //stage.TrySpawnEnemy();
    Destroy(gameObject);
  }

  private void TakeDamage() {
    health -= 1;
  }

  private void OnCollisionEnter(Collision collision) {
    if (!collisions.Contains(collision.gameObject.layer)) {
     collisions.Add(collision.gameObject.layer);
    }
    if (IsRadiateShield(collision.gameObject)) {
      gameObject.layer = LayerMask.NameToLayer("BlownEnemy");
    }
    if (IsRadiateShield(collision.gameObject) || IsWall(collision.gameObject)) {
      if (IsSandwiched()) {
        TakeDamage();
      }
    }
  }

  private void OnCollisionExit(Collision collision) {
    collisions.Remove(collision.gameObject.layer);
    if (IsRadiateShield(collision.gameObject)) {
      gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
  }

  private void OnTriggerEnterDetectionArea(Collider other) {
    if (IsPlayer(other.gameObject)) {
      target = other.gameObject;
      mode = Mode.Approach;
    }
  }

  private void OnTriggerExitDetectionArea(Collider other) {
    if (IsPlayer(other.gameObject)) {
      remainingTimeToChase = durationToChase;
      mode = Mode.Chase;
    }
  }

  private void OnTriggerEnterFiringArea(Collider other) {
    if (IsPlayer(other.gameObject)) {
      target = other.gameObject;
      if (CanSeeTarget()) {
        agent.updatePosition = false;
        mode = Mode.Attack;
      }
    }
  }

  private void OnTriggerStayFiringArea(Collider other) {
    if (IsPlayer(other.gameObject)) {
      if (CanSeeTarget()) {
        agent.nextPosition = transform.position;
        agent.updatePosition = false;
        mode = Mode.Attack;
      }
      else {
        mode = Mode.Approach;
        agent.nextPosition = transform.position;
        agent.updatePosition = true;
      }
    }
  }

  private void OnTriggerExitFiringArea(Collider other) {
    if (IsPlayer(other.gameObject)) {
      agent.nextPosition = transform.position;
      agent.updatePosition = true;
      mode = Mode.Approach;
    }
  }

  private bool IsSandwiched() {
    var wallLayer = LayerMask.NameToLayer("Wall");
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    if (collisions.Contains(radiateShieldLayer) && collisions.Contains(wallLayer)) {
      return true;
    }
    return false;
  }

  private bool IsPlayer(GameObject other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) return true;
    return false;
  }

  private bool IsRadiateShield(GameObject other) {
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    if (other.gameObject.layer == radiateShieldLayer) return true;
    return false;
  }

  private bool IsWall(GameObject other) {
    var wallLayer = LayerMask.NameToLayer("Wall");
    if (other.gameObject.layer == wallLayer) return true;
    return false;
  }
}
