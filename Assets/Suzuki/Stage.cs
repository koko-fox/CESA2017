using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
  [SerializeField]
  private GameObject enemyPrefab;
  [SerializeField]
  private float spawnWait;
  [SerializeField]
  private GameObject[] spawnPoints;

  private void Start() {
    for (int i = 0; i < 10; ++i) {
      var spawnPoint = spawnPoints[i];
      var position = spawnPoint.transform.position;
      var rotation = spawnPoint.transform.rotation;
      var enemy = Instantiate(enemyPrefab, position, rotation, spawnPoint.transform) as GameObject;
      var enemyBehavior = enemy.GetComponent<Enemy>();
      enemyBehavior.OnKilled += TrySpawnEnemy2;
    }
  }

  private void TrySpawnEnemy2(Enemy enemy) {
    foreach (var spawnPoint in spawnPoints) {
      if (spawnPoint.transform.childCount == 0) {
        var position = spawnPoint.transform.position;
        var rotation = spawnPoint.transform.rotation;
        var newEnemy = Instantiate(enemyPrefab, position, rotation, spawnPoint.transform) as GameObject;
        var enemyBehavior = newEnemy.GetComponent<Enemy>();
        enemyBehavior.OnKilled += TrySpawnEnemy2;
        break;
      }
    }
  }

  public void TrySpawnEnemy() {
    foreach (var spawnPoint in spawnPoints) {
      if (spawnPoint.transform.childCount == 0) {
        var position = spawnPoint.transform.position;
        var rotation = spawnPoint.transform.rotation;
        var enemy = Instantiate(enemyPrefab, position, rotation, spawnPoint.transform) as GameObject;
        var enemyBehavior = enemy.GetComponent<Enemy>();
        enemyBehavior.Stage = this;
        break;
      }
    }
    //StartCoroutine("Spawn");
  }

  private IEnumerator Spawn() {
    yield return new WaitForSeconds(spawnWait);
    var position = transform.position;
    position.x += Random.Range(-1, 1) * 2.0f;
    position.z += Random.Range(-1, 1) * 2.0f;
    var rotation = transform.rotation;
    var newEnemy = Instantiate(enemyPrefab, position, rotation) as GameObject;
    var enemyScript = newEnemy.GetComponent<Enemy>();
    enemyScript.Stage = this;
  }

}
