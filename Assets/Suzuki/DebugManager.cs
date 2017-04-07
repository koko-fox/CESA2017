using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {
  [SerializeField]
  private GameObject player;
  [SerializeField]
  private GameObject shieldPrefab;

  private void Update() {
    if (Input.GetKeyDown(KeyCode.F)) {
      var shield = Instantiate(shieldPrefab);
      shield.transform.position = player.transform.position + player.transform.forward * 1.0f;
      shield.transform.rotation = player.transform.rotation;
    }
  }

}
