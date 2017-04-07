using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {
  [SerializeField]
  private GameObject enemyPrefab;
  [SerializeField]
  private float spawnWait;

  private void Start() {
    TrySpawnEnemy();
    TrySpawnEnemy();
    TrySpawnEnemy();
  }
  public void TrySpawnEnemy() {
    StartCoroutine("Spawn");
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
