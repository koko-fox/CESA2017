using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour {
  [SerializeField]
  private GameObject bulletPrefab;
  [SerializeField]
  private int roundPerSecond;

  private const float reloadTime = 4.0f;
  [SerializeField]
  private float coolTime = 0.0f;

  private bool isFiring = false;

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
      SpawnBullet();
      yield return new WaitForSeconds(1.0f / roundPerSecond);
    }
    coolTime = reloadTime;
    isFiring = false;
  }

  private void SpawnBullet() {
    Instantiate(bulletPrefab, transform.position, transform.rotation);
  }

}
