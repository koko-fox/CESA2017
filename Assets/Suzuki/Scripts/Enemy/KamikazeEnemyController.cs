﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleBehaviourTree;

public class KamikazeEnemyController : EnemyModuleBase {
  private Transform target;
  private EnemyStatus status;
  private BehaviourTree behaviourTree;
  private new Rigidbody rigidbody;
  private bool isTargetInDetectionArea;
  public bool isFlying = false;

  [SerializeField]
  private Sensor detectionArea;

  protected override void DoAwake() {
    rigidbody = GetComponent<Rigidbody>();
  }

  protected override void DoStart() {
    detectionArea.onSensorEnter += DetectionArea_OnSensorEnter;

    BuildBehaviourTree();
  }

  protected override void DoUpdate() {
    behaviourTree.Update();
  }

  protected override void DoDied() {
    Destroy(detectionArea);
  }

  private void DetectionArea_OnSensorEnter(Collider other) {
    if (isTargetInDetectionArea) return;
    if (IsPlayer(other.gameObject)) {
      target = other.gameObject.transform;
      isTargetInDetectionArea = true;
    }
  }

  private void Kamikaze() {
    var angle = ElevationAngle(target) + 35.0f;
    angle = Mathf.Clamp(angle, 35.0f, 85.0f);
    rigidbody.velocity = BallisticVel(target, angle);
    isFlying = true;
  }

  private Vector3 BallisticVel(Transform target, float angle) {
    var dir = (target.position - transform.position);
    var height = dir.y;
    var distance = dir.magnitude;
    var a = angle * Mathf.Deg2Rad;
    dir.y = distance * Mathf.Tan(a);
    distance += height / Mathf.Tan(a);
    var vel = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));

    return vel * dir.normalized;
  }

  private float ElevationAngle(Transform target) {
    var dir = (target.position - transform.position);
    var dirH = new Vector3(dir.x, 0.0f, dir.y);
    var angle = Vector3.Angle(dir, dirH);
    if (dir.y < 0.0f) {
      angle = -angle;
    }
    return angle;
  }

  private void Wait() {

  }

  private bool IsPlayer(GameObject other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) return true;
    return false;
  }

  private void BuildBehaviourTree() {
    var kamikazeAction = new ActionNode(Kamikaze);
    var canKamikazeDecorator = new DecoratorNode(kamikazeAction, () => isTargetInDetectionArea);
    var waitAction = new ActionNode(Wait);
    var selector1 = new SelectorNode(canKamikazeDecorator, waitAction);
    var isFlyingDecorator = new DecoratorNode(waitAction, () => isFlying);
    var selector2 = new SelectorNode(isFlyingDecorator, selector1);
    var isBlownDecorator = new DecoratorNode(waitAction, () => core.isBlown);
    var selector3 = new SelectorNode(isBlownDecorator, selector2);
    behaviourTree = new BehaviourTree(selector3);
  }

}
