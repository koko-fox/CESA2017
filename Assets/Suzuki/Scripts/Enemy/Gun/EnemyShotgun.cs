using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : EnemyGun {
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
  [SerializeField]
  private int bulletsPerFire = 0;

  private void Update() {
    coolTime -= Time.deltaTime;
  }

  public override void Fire() {
    audioSource.PlayOneShot(shootSound);
    coolTime = reloadTime;
    for (int i = 0; i < bulletsPerFire; ++i) {
      var direction = Vector3.forward;
      var randomizedDirection = UnityEngine.Random.insideUnitCircle * groupingRate;
      direction.x += randomizedDirection.x;
      direction.y += randomizedDirection.y;
      direction = transform.rotation * direction;

      var newBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
      var bulletBehaviour = newBullet.GetComponent<EnemyBullet>();
      var lookRotation = Quaternion.LookRotation(direction);
      newBullet.transform.rotation = lookRotation;
    }
  }

  public override bool CanFire() {
    if (coolTime > 0.0f) return false;
    return true;
  }
}
