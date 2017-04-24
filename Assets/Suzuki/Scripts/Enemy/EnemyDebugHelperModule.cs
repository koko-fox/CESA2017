using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent
, RequireComponent(typeof(EnemyCore))
]
public class EnemyDebugHelperModule : MonoBehaviour {
  private EnemyCore core;
  [SerializeField]
  private float rayLength;
  [SerializeField]
  private Color rayColor;
  private void Awake() {
    core = GetComponent<EnemyCore>();
  }
  private void Update() {
    core.onUpdated += () => { Debug.DrawRay(transform.position, transform.forward * rayLength, rayColor); };
  }
}
