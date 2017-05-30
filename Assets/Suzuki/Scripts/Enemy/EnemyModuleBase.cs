using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyCore))]
public abstract class EnemyModuleBase : MonoBehaviour {
  protected EnemyCore core;

  private void Awake() {
    core = GetComponent<EnemyCore>();
    DoAwake();
  }

  private void Start() {
    core.onUpdated += OnUpdated;
    core.onFixedUpdated += OnFixedUpdate;
    core.onDied += OnDied;
    DoStart();
  }

  private void OnUpdated() {
    DoUpdate();
  }

  private void OnFixedUpdate() {
    DoFixedUpdate();
  }

  private void OnDied (EnemyCore.DiedFactor factor) {
    core.onUpdated -= OnUpdated;
    core.onFixedUpdated -= OnFixedUpdate;
    core.onDied -= OnDied;
    DoDied();
    Destroy(this);
  }

  protected virtual void DoAwake() { }
  protected virtual void DoStart() { }
  protected virtual void DoUpdate() { }
  protected virtual void DoFixedUpdate() { }
  protected virtual void DoDied() { }

}
