using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  [SerializeField]
  private float remainingTime;
  private float startingTime;
  private float elapsedTimeFromStarting;
  private Text time;

  public float ElapsedTime {
    get { return elapsedTimeFromStarting; }
  }

  private void Start() {
    time = GetComponent<Text>();
  }

  private void Update() {
    elapsedTimeFromStarting = Time.time - startingTime;
    remainingTime -= Time.deltaTime;
    if (remainingTime < 0.0f) {
      remainingTime = 0.0f;
    }
    int min = (int)(remainingTime / 60) % 60;
    int sec = (int)(remainingTime) % 60;
    int msec = (int)(remainingTime * 100) % 100;
    time.text = min.ToString("D2") + "'" + sec.ToString("D2") + "\"" + msec.ToString("D2");
  }

}