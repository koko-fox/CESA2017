using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
  public delegate void OnSensorEnterEvent(Collider other);
  public delegate void OnSensorStayEvent(Collider other);
  public delegate void OnSensorExitEvent(Collider other);

  public event OnSensorEnterEvent onSensorEnter = delegate { };
  public event OnSensorStayEvent onSensorStay = delegate { };
  public event OnSensorExitEvent onSensorExit = delegate { };

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
