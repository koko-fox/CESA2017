using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
  [SerializeField]
  private GameObject bulletPrefab;

  private const float reloadTime = 4.0f;
  [SerializeField]
  private float coolTime = 0.0f;

  public void Fire() {
    if (coolTime < 0.0f) {
      var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
      coolTime = reloadTime;
    }
  }

  private void Update() {
    coolTime -= Time.deltaTime;
  }

}
