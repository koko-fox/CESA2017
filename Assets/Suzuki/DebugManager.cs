using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {
  [SerializeField]
  private GameObject player;
  [SerializeField]
  private GameObject wallPrefab;

  private void Update() {
    if (Input.GetKeyDown(KeyCode.K)) {
      var position = player.transform.position + player.transform.forward * 2.0f;
      var rotation = player.transform.rotation;
      var wall = Instantiate(wallPrefab, position, rotation) as GameObject;
      var rigidBody = wall.GetComponent<Rigidbody>();
      rigidBody.AddForce(player.transform.forward * 100, ForceMode.Impulse);
    }
  }

}
