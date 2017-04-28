using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {
  private Text time;
  [SerializeField]
  private Timer timer;

  private void Awake() {
    time = GetComponent<Text>();
  }

  private void Start() {
    timer.onValueChanged += Timer_onValueChanged;
  }

  private void Timer_onValueChanged(float value) {
    int min = (int)(value / 60) % 60;
    int sec = (int)(value) % 60;
    int msec = (int)(value * 100) % 100;
    time.text = min.ToString("D2") + "'" + sec.ToString("D2") + "\"" + msec.ToString("D2");
  }
}
