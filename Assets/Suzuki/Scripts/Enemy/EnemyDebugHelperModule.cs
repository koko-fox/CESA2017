using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyDebugHelperModule : EnemyModuleBase {
  [SerializeField]
  private float rayLength;
  [SerializeField]
  private Color rayColor;

  protected override void DoUpdate() {
    Debug.DrawRay(transform.position, transform.forward * rayLength, rayColor);
  }
}
