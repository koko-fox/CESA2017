using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyCore : MonoBehaviour {
  public delegate void UpdateEvent();
  public delegate void FixedUpdateEvent();
  public event UpdateEvent onUpdated = delegate { };
  public event FixedUpdateEvent onFixedUpdate = delegate { };

  public bool isBlown { get; private set; }
  private List<int> collisions = new List<int>();
  private RadiateShieldController collidedShield;
  [SerializeField]
  private float health;

  private void Update() {
    onUpdated();
  }

  private void FixedUpdate() {
    if (collidedShield) {
      gameObject.layer = LayerMask.NameToLayer("BlownEnemy");
      isBlown = true;
      var wallLayer = LayerMask.NameToLayer("Wall");
      if (collisions.Contains(wallLayer)) {
        ApplyDamage();
      }
    }
    else {
      isBlown = false;
      gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    onFixedUpdate();
    collisions.Clear();
  }

  private void OnCollisionStay(Collision collision) {
    collisions.Add(collision.gameObject.layer);
    if (IsRadiateShield(collision.gameObject)) {
      collidedShield = collision.gameObject.GetComponent<RadiateShieldController>();
    }
  }

  private void ApplyDamage() {
    health -= collidedShield.AttackPower * Time.fixedDeltaTime;
  }

  private bool IsRadiateShield(GameObject other) {
    var radiateShieldLayer = LayerMask.NameToLayer("RadiateShield");
    if (other.gameObject.layer == radiateShieldLayer) return true;
    return false;
  }

}
