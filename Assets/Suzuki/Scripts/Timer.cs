using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  public delegate void ValueChangeEvent(float value);
  public delegate void ValueAddEvent(float value);
  public event ValueChangeEvent onValueChanged = delegate { };
  public event ValueAddEvent onValueAdded = delegate { };

  [SerializeField]
  private float remainingTime;
  public float RemainingTime {
    get { return remainingTime; }
    set {
      var oldValue = remainingTime;
      remainingTime = value;
      var dif = remainingTime - oldValue;
      if (dif > 0.0f) {
        onValueAdded(dif);
      }
    }
  }

  private void Update() {
    RemainingTime -= Time.deltaTime;
    if (RemainingTime < 0.0f) {
      RemainingTime = 0.0f;
    }
    onValueChanged(RemainingTime);
  }

}