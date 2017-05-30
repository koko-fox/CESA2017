using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticlePlayground;

[DisallowMultipleComponent]
public class EnemyCore : MonoBehaviour {
  public delegate void UpdateEvent();
  public delegate void FixedUpdateEvent();
  public delegate void DiedEvent();
  public delegate void DamagedEvent(float value);
  public event UpdateEvent onUpdated = delegate { };
  public event FixedUpdateEvent onFixedUpdated = delegate { };
  public event DiedEvent onDied = delegate { };
  public event DamagedEvent onDamaged = delegate { };

  public bool isBlown { get; private set; }
  private bool diedMark = false;
  private List<int> collisions = new List<int>();
  private ShieldCore collidedShield;
  [SerializeField]
  private float health;
  [SerializeField]
  private AudioClip diedSound;
  [SerializeField]
  private PlaygroundParticlesC particle;

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
    onDamaged(damageValue);
    if (health > 0.0f) return;
    Die();
  }

  public void Die() {
    if (diedMark) return;
    Destroy(GetComponent<Rigidbody>());
    Destroy(GetComponent<Collider>());
    onDied();
    StartCoroutine(ToDie());
    diedMark = true;
  }

  private bool IsRadiateShield(GameObject other) {
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    if (other.gameObject.layer == radiateShieldLayer) return true;
    return false;
  }

  private IEnumerator ToDie() {
    Vector3 scaleOrigin = transform.localScale;
    float t = 0.0f;
    while (transform.localScale.x > 0.0f) {
      yield return null;
      t += Time.deltaTime * 2.0f;
      var scale = Vector3.MoveTowards(scaleOrigin, Vector3.zero, t);
      scale.y = scaleOrigin.y;
      transform.localScale = scale;
    }
    //yield return new WaitForSeconds(1.0f);
    AudioSource.PlayClipAtPoint(diedSound, transform.position);
    Destroy(gameObject);
  }

}
