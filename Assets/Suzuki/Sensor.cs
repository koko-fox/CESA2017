using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
  public delegate void OnSensorEnterEvent(Collider other);
  public delegate void OnSensorExitEvent(Collider other);

  private OnSensorEnterEvent onSensorEnter;
  private OnSensorExitEvent onSensorExit;

  public OnSensorEnterEvent OnSensorEnter {
    get {
      return onSensorEnter;
    }

    set {
      onSensorEnter = value;
    }
  }

  public OnSensorExitEvent OnSensorExit {
    get {
      return onSensorExit;
    }

    set {
      onSensorExit = value;
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (onSensorEnter != null) {
      onSensorEnter(other);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (onSensorExit != null) {
      onSensorExit(other);
    }
  }


}
