using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour {
  [SerializeField]
  private HanachanCore player;
  [SerializeField]
  private Timer timer;

  private void Start() {
    timer.onValueChanged += Timer_onValueChanged;
  }

  private void Timer_onValueChanged(float value) {
    if (value <= 0.0f) {
      SceneManager.LoadScene(2);
    }
  }
}
