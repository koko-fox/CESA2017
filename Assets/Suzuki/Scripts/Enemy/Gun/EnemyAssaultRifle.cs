using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAssaultRifle : EnemyGun {
  private bool isFiring = false;

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
  [SerializeField]
  private float groupingRate;
  [SerializeField]
  private GameObject bullet;
  [SerializeField]
  private float firePower;

  public override bool CanFire() {
    if (isFiring) return false;
    if (coolTime > 0.0f) return false;
    return true;
  }

  public override void Fire() {
    if (!CanFire()) return;
    isFiring = true;
    StartCoroutine("ThreeRoundBurst");
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
    //audioSource.PlayOneShot(shootSound);
    //RaycastHit hitinfo;
    //var origin = transform.position;

    //var direction = Vector3.forward;
    //var randomizedDirection = UnityEngine.Random.insideUnitCircle * groupingRate;
    //direction.x += randomizedDirection.x;
    //direction.y += randomizedDirection.y;
    //direction = transform.rotation * direction;

    //var layerMask = LayerMask.GetMask("Enemy", "BlownEnemy", "ItemOrb");
    //layerMask = ~layerMask;
    //if (Physics.Raycast(origin, direction, out hitinfo, 1000.0f, layerMask)) {
    //  var otherObject = hitinfo.collider.gameObject;
    //  var playerLayer = LayerMask.NameToLayer("UnityChan");
    //  if (otherObject.layer == playerLayer) {
    //    var player = otherObject.GetComponent<HanachanCore>();
    //    player.Health -= firePower;
    //  }
    //  var shieldLayer = LayerMask.NameToLayer("RadiateShield");
    //  if (otherObject.layer == shieldLayer) {
    //    var shieldScript = otherObject.GetComponent<RadShieldCore>();
    //    shieldScript.NoticeHitBullet(hitinfo.point);
    //  }
    //}

    //var newBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
    //var bulletBehaviour = newBullet.GetComponent<EnemyBullet>();
    //var lookRotation = Quaternion.LookRotation(direction);
    //newBullet.transform.rotation = Quaternion.AngleAxis(90.0f, transform.right);
    //newBullet.transform.rotation *= lookRotation;
    //bulletBehaviour.direction = direction;
  }

}

