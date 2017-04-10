using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
  public delegate void OnSensorEnterEvent(Collider other);
  public delegate void OnSensorStayEvent(Collider other);
  public delegate void OnSensorExitEvent(Collider other);

  private OnSensorEnterEvent onSensorEnter;
  private OnSensorStayEvent onSensorStay;
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

  public OnSensorStayEvent OnSensorStay {
    get {
      return onSensorStay;
    }

    set {
      onSensorStay = value;
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (onSensorEnter != null) {
      onSensorEnter(other);
    }
  }

  private void OnTriggerStay(Collider other) {
    if (onSensorStay != null) {
      onSensorStay(other);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (onSensorExit != null) {
      onSensorExit(other);
    }
  }


}
