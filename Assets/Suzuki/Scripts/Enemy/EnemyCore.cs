using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

[DisallowMultipleComponent]
public class EnemyCore : MonoBehaviour {
  public delegate void UpdateEvent();
  public delegate void FixedUpdateEvent();
  public delegate void DiedEvent(DeathInfo info);
  public delegate void DamagedEvent(float value);
  public event UpdateEvent onUpdated = delegate { };
  public event FixedUpdateEvent onFixedUpdated = delegate { };
  public event DiedEvent onDied = delegate { };
  public event DamagedEvent onDamaged = delegate { };

  public class DeathInfo {
    public enum DeathFactor {
      KilledByPlayer,
      Suicided
    }

    private EnemyCore enemy;
    private DeathFactor factor;

    public EnemyCore Enemy { get; set; }
    public DeathFactor Factor { get; set; }
  }

  public bool isBlown { get; private set; }
  private bool diedMark = false;
  private List<int> collisions = new List<int>();
  private ShieldCore collidedShield;
  private float health;
  private float prevHealth;
  [SerializeField]
  private float maxHealth;
  [SerializeField]
  private TFSound.AudioProperty diedSound;
  [SerializeField]
  private TFSound.AudioProperty damagedSound;
  [SerializeField]
  private PlaygroundParticlesC particle;
  [SerializeField]
  private float[] damagedSoundRate;

  private void Awake() {
    health = maxHealth;
    prevHealth = health;
  }

  private void Update() {
    onUpdated();
  }

  private void FixedUpdate() {
    if (collidedShield) {
      gameObject.layer = LayerMask.NameToLayer("BlownEnemy");
      isBlown = true;
      var wallLayer = LayerMask.NameToLayer("Wall");
      if (collisions.Contains(wallLayer)) {
        if (health > 0.0f) {
          ApplyDamage();
        }
      }
    }
    else {
      isBlown = false;
      gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    onFixedUpdated();
    collidedShield = null;
    collisions.Clear();
  }

  private void OnCollisionStay(Collision collision) {
    collisions.Add(collision.gameObject.layer);
    if (IsRadiateShield(collision.gameObject)) {
      collidedShield = collision.gameObject.GetComponent<ShieldCore>();
    }
  }

  private void ApplyDamage() {
    var damageValue = collidedShield.attackSystem.lastValue * Time.fixedDeltaTime;
    health -= damageValue;
    foreach (var elem in damagedSoundRate) {
      var rate = health / maxHealth;
      var prevRate = prevHealth / maxHealth;
      if (rate <= elem && prevRate > elem) {
        TFSound.Play(damagedSound, transform);
        break;
      }
    }
    onDamaged(damageValue);
    prevHealth = health;
    if (health > 0.0f) return;
    var info = new DeathInfo();
    info.Enemy = this;
    info.Factor = DeathInfo.DeathFactor.KilledByPlayer;
    Die(info);
  }

  public void Die(DeathInfo info) {
    if (diedMark) return;
    Destroy(GetComponent<Rigidbody>());
    Destroy(GetComponent<Collider>());
    onDied(info);
    StartCoroutine(ToDie());
    diedMark = true;
  }

  private bool IsRadiateShield(GameObject other) {
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    if (other.gameObject.layer == radiateShieldLayer) return true;
    return false;
  }

  private IEnumerator ToDie() {
    TFSound.Play(diedSound, transform);
    Vector3 scaleOrigin = transform.localScale;
    float t = 0.0f;
    while (transform.localScale.x > 0.0f) {
      yield return null;
      t += Time.deltaTime * 2.0f;
      var scale = Vector3.MoveTowards(scaleOrigin, Vector3.zero, t);
      scale.y = scaleOrigin.y;
      transform.localScale = scale;
    }
    Destroy(gameObject);
  }

}
