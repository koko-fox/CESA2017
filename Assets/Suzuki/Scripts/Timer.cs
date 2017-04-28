using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  public delegate void ValueChangeEvent(float value);
  public event ValueChangeEvent onValueChanged = delegate { };

  [SerializeField]
  private float remainingTime;

  private void Update() {
    remainingTime -= Time.deltaTime;
    if (remainingTime < 0.0f) {
      remainingTime = 0.0f;
    }
    onValueChanged(remainingTime);
  }

}