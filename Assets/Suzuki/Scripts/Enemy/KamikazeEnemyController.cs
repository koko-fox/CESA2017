﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleBehaviourTree;

public class KamikazeEnemyController : EnemyModuleBase {
  private Vector3 targetPosition;
  private EnemyStatus status;
  private BehaviourTree behaviourTree;
  private new Rigidbody rigidbody;
  private bool isTargetInDetectionArea;
  public bool IsFlying { get; private set; }
  public bool IsPreparing { get; private set; }

  [SerializeField]
  private Sensor detectionArea;
  [SerializeField]
  private float preparationTime;
  [SerializeField]
  private GameObject targetMarker;

  private GameObject marker;

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
    Destroy(marker);
  }

  private void DetectionArea_OnSensorEnter(Collider other) {
    if (isTargetInDetectionArea) return;
    if (IsPlayer(other.gameObject)) {
      targetPosition = other.gameObject.transform.position;
      isTargetInDetectionArea = true;
    }
  }

  private void Kamikaze() {
    var angle = ElevationAngle(targetPosition) + 35.0f;
    angle = Mathf.Clamp(angle, 35.0f, 85.0f);
    rigidbody.velocity = BallisticVel(targetPosition, angle);
    IsPreparing = false;
    IsFlying = true;
  }

  private Vector3 BallisticVel(Vector3 targetPosition, float angle) {
    var dir = (targetPosition - transform.position);
    var height = dir.y;
    var distance = dir.magnitude;
    var a = angle * Mathf.Deg2Rad;
    dir.y = distance * Mathf.Tan(a);
    distance += height / Mathf.Tan(a);
    var vel = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));

    return vel * dir.normalized;
  }

  private float ElevationAngle(Vector3 targetPosition) {
    var dir = (targetPosition - transform.position);
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

  private void PrepareForKamikaze() {
    var position = targetPosition + new Vector3(0.0f, 0.1f, 0.0f);
    marker = Instantiate(targetMarker, targetPosition, Quaternion.AngleAxis(-90.0f, Vector3.right));
    IsPreparing = true;
  }

  private void ProcPreparation() {
    preparationTime -= Time.deltaTime;
  }

  private void BuildBehaviourTree() {
    var prepareAction = new ActionNode(PrepareForKamikaze);
    var waitAction = new ActionNode(Wait);
    var canPrepareDecorator =  new DecoratorNode(prepareAction, () => isTargetInDetectionArea);
    var selector1 = new SelectorNode(canPrepareDecorator, waitAction);

    var kamikazeAction = new ActionNode(Kamikaze);
    var procPrepareAction = new ActionNode(ProcPreparation);
    var canKamikazeDecorator = new DecoratorNode(kamikazeAction, () => { return preparationTime < 0.0f; });
    var selector2 = new SelectorNode(canKamikazeDecorator, procPrepareAction);

    var isPreparingDecorator = new DecoratorNode(selector1, () => !IsPreparing);
    var selector3 = new SelectorNode(isPreparingDecorator, selector2);

    var isFlyingDecorator = new DecoratorNode(waitAction, () => IsFlying);
    var selector4 = new SelectorNode(isFlyingDecorator, selector3);

    var isBlownDecorator = new DecoratorNode(waitAction, () => core.isBlown);
    var selector5 = new SelectorNode(isBlownDecorator, selector4);

    behaviourTree = new BehaviourTree(selector5);
  }

}
