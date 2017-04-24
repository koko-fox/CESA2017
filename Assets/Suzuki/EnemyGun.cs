using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
  [SerializeField]
  private int roundPerSecond;
  [SerializeField]
  private float reloadTime = 4.0f;
  [SerializeField]
  private float coolTime = 0.0f;
  [SerializeField]
  private AudioSource audioSource;
  [SerializeField]
  private AudioClip shootSound;
  [SerializeField]
  private AudioClip reloadSound;

  private bool isFiring = false;

  public bool CanFire() {
    if (isFiring) return false;
    if (coolTime > 0.0f) return false;
    return true;
  }

  public void Fire() {
    if (isFiring) return;
    if (coolTime < 0.0f) {
      isFiring = true;
      StartCoroutine("ThreeRoundBurst");
    }
  }

  private void Update() {
    coolTime -= Time.deltaTime;
  }

  private IEnumerator ThreeRoundBurst() {
    for (int i = 0; i < 3; ++i) {
      Shoot();
      yield return new WaitForSeconds(1.0f / roundPerSecond);
    }
    AudioSource.PlayClipAtPoint(reloadSound, transform.position);
    coolTime = reloadTime;
    isFiring = false;
  }

  private void Shoot() {
    audioSource.PlayOneShot(shootSound);
    RaycastHit hitinfo;
    var origin = transform.position;
    var direction = transform.forward;
    var layerMask = LayerMask.GetMask("Enemy", "BlownEnemy", "ItemOrb");
    layerMask = ~layerMask;
    if (Physics.Raycast(origin, direction, out hitinfo, 1000.0f, layerMask)) {
      var otherObject = hitinfo.collider.gameObject;
      var playerLayer = LayerMask.NameToLayer("UnityChan");
      if (otherObject.layer == playerLayer) {
        // TODO: プレイヤーにダメージを与える処理
        Debug.Log("Hit to player");
      }
      var shieldLayer = LayerMask.NameToLayer("RadiateShield");
      if (otherObject.layer == shieldLayer) {
        // TODO: シールドに当たったときの処理
        var shieldScript = otherObject.GetComponent<RadiateShieldController>();
        shieldScript.PlayHitSound();
        Debug.Log("Hit to shield");
      }
    }
  }
}
