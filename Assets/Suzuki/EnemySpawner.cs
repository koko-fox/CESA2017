using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  [SerializeField]
  private GameObject enemyPrefab;
  [SerializeField]
  private float spawnWait;

  private void Start() {
    StartCoroutine("Spawn");
  }

  private IEnumerator Spawn() {
    yield return new WaitForSeconds(spawnWait);
    Instantiate(enemyPrefab, transform.position, transform.rotation);
    Destroy(gameObject);
  }

}
