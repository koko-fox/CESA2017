using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
  public delegate void OnSensorEnterEvent(Collider other);
  public delegate void OnSensorStayEvent(Collider other);
  public delegate void OnSensorExitEvent(Collider other);

  private event OnSensorEnterEvent onSensorEnter = delegate { };
  private event OnSensorStayEvent onSensorStay = delegate { };
  private event OnSensorExitEvent onSensorExit = delegate { };

  public event OnSensorEnterEvent OnSensorEnter;
  public event OnSensorExitEvent OnSensorExit;
  public event OnSensorStayEvent OnSensorStay;

  private void OnTriggerEnter(Collider other) {
    onSensorEnter(other);
  }

  private void OnTriggerStay(Collider other) {
    onSensorStay(other);
  }

  private void OnTriggerExit(Collider other) {
    onSensorExit(other);
  }
}
