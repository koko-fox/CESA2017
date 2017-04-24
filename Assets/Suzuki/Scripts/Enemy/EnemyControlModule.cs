﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleBehaviourTree;
using System;

[DisallowMultipleComponent
, RequireComponent(typeof(EnemyCore))
]
public class EnemyControlModule : MonoBehaviour {
  private EnemyCore core;
  private GameObject target;
  private NavMeshAgent agent;
  private EnemyStatus status;
  private BehaviourTree behaviourTree;
  private bool isTargetInRange;
  private bool isTargetInDetectionArea;
  private float remainingTimeToChase;

  [SerializeField]
  private Sensor detectionArea;
  [SerializeField]
  private Sensor firingArea;
  [SerializeField]
  private float durationToChase = 3.0f;

  private void Awake() {
    core = GetComponent<EnemyCore>();
    agent = GetComponent<NavMeshAgent>();
    status = GetComponent<EnemyStatus>();
  }

  private void Start() {
    core.onUpdated += Core_onUpdated;
    detectionArea.onSensorEnter += DetectionArea_OnSensorEnter;
    detectionArea.onSensorExit += DetectionArea_OnSensorExit;
    firingArea.onSensorEnter += FiringArea_onSensorEnter;
    firingArea.onSensorExit += FiringArea_onSensorExit;
    agent.speed = status.MoveSpeed;
    agent.angularSpeed = status.RotationSpeed;
    agent.updatePosition = false;
    agent.updateRotation = false;

    var chaseAction = new ActionNode(Chase);
    var canChaseDecorator = new DecoratorNode(chaseAction, () => remainingTimeToChase > 0.0f);
    var approachAction = new ActionNode(ApproachToTarget);
    var canApproachDecorator = new DecoratorNode(approachAction, () => isTargetInDetectionArea);
    var selector1 = new SelectorNode(canApproachDecorator, canChaseDecorator);
    var fireAction = new ActionNode(Fire);
    var canFireDecorator = new DecoratorNode(fireAction, () => {  return true; });
    var faceToTargetAction = new ActionNode(FaceToTarget);
    var selector2 = new SelectorNode(canFireDecorator, faceToTargetAction);
    var canSeeTargetDecorator = new DecoratorNode(selector2, CanSeeTarget);
    var selector3 = new SelectorNode(canSeeTargetDecorator, approachAction);
    var isTargetInRangeDecorator = new DecoratorNode(selector3, () => isTargetInRange);
    var selector4 = new SelectorNode(isTargetInRangeDecorator, selector1);
    var isTargetNullDecorator = new DecoratorNode(selector4, () => { return target != null; });
    var waitAction = new ActionNode(Wait);
    var selector5 = new SelectorNode(isTargetNullDecorator, waitAction);
    var blownChecker = new DecoratorNode(waitAction, () => { return core.isBlown; });
    var selector6 = new SelectorNode(blownChecker, selector5);
    behaviourTree = new BehaviourTree(selector6);
  }

  private void Chase() {
    ApproachToTarget();
    remainingTimeToChase -= Time.deltaTime;
    if (remainingTimeToChase < 0.0f) {
      target = null;
    }
  }

  private void Fire() {
    FaceToTarget();
  }

  private void Core_onUpdated() {
    if (target) {
      agent.destination = target.transform.position;
    }
    behaviourTree.Update();
  }

  private void Wait() {
  }

  private void ApproachToTarget() {
    agent.destination = target.transform.position;
    transform.position += agent.desiredVelocity * Time.deltaTime;
    FaceToTarget();
  }

  private void FaceToTarget() {
    var direction = (target.transform.position - transform.position).normalized;
    direction.y = 0.0f;
    var lookRotation = Quaternion.LookRotation(direction);
    transform.rotation = lookRotation;
  }

  private void DetectionArea_OnSensorEnter(Collider other) {
    if (IsPlayer(other.gameObject)) {
      target = other.gameObject;
      agent.nextPosition = transform.position;
      isTargetInDetectionArea = true;
    }
  }

  private void DetectionArea_OnSensorExit(Collider other) {
    if (IsPlayer(other.gameObject)) {
      remainingTimeToChase = durationToChase;
      isTargetInDetectionArea = false;
    }
  }

  private void FiringArea_onSensorEnter(Collider other) {
    if (IsPlayer(other.gameObject)) {
      isTargetInRange = true;
    }
  }

  private void FiringArea_onSensorExit(Collider other) {
    if (IsPlayer(other.gameObject)) {
      agent.nextPosition = transform.position;
      isTargetInRange = false;
    }
  }

  private bool IsPlayer(GameObject other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) return true;
    return false;
  }

  private bool CanSeeTarget() {
    if (target == null) return false;
    NavMeshHit hitInfo;
    if (agent.Raycast(target.transform.position, out hitInfo)) return false;
    return true;
  }

}
