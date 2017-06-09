using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
  public delegate void EnemyKilledEvent();
  public event EnemyKilledEvent onEnemyKilled = delegate { };
  public delegate void EnemySpawnedEvent(EnemyCore enemy);
  public event EnemySpawnedEvent onEnemySpawned = delegate { };

  [SerializeField]
  private GameObject[] enemyPrefabs;
  [SerializeField]
  private float spawnWait;
  [SerializeField]
  private GameObject[] spawnPoints;
  [SerializeField]
  private int enemies;

  private void Start() {
    Debug.Assert(enemies < spawnPoints.Length);
    for (int i = 0; i < enemies; ++i) {
      SpawnEnemy();
    }
  }

  private void SpawnEnemy() {
    while (true) {
      var index = Random.Range(0, spawnPoints.Length);
      var spawnPoint = spawnPoints[index];
      if (spawnPoint.transform.childCount == 0) {
        var position = spawnPoint.transform.position;
        var rotation = spawnPoint.transform.rotation;
        var prefabIndex = Random.Range(0, enemyPrefabs.Length);
        var enemyPrefab = enemyPrefabs[prefabIndex];
        var newEnemy = Instantiate(enemyPrefab, position, rotation, spawnPoint.transform) as GameObject;
        var enemyBehavior = newEnemy.GetComponent<EnemyCore>();
        onEnemySpawned(enemyBehavior);
        enemyBehavior.onDied += (EnemyCore.DiedFactor factor) => {
          StartCoroutine(Spawn());
          onEnemyKilled();
        };
        break;
      }
    }
  }

  private IEnumerator Spawn() {
    yield return new WaitForSeconds(spawnWait);
    SpawnEnemy();
  }
}
