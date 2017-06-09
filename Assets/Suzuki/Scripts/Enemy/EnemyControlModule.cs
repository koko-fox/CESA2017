using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleBehaviourTree;
using System;

[DisallowMultipleComponent]
public class EnemyControlModule : EnemyModuleBase {
  private GameObject target;
  private NavMeshAgent agent;
  private EnemyStatus status;
  private BehaviourTree behaviourTree;
  private bool isTargetInRange;
  private bool isTargetInDetectionArea;
  private float remainingTimeToChase;
  private bool isFighting;
  private bool isJumping;

  [SerializeField]
  private Sensor detectionArea;
  [SerializeField]
  private Sensor firingArea;
  [SerializeField]
  private EnemyGun gun;
  [SerializeField]
  private float durationToChase = 3.0f;
  [SerializeField]
  private Animator animator;

  protected override void DoAwake() {
    agent = GetComponent<NavMeshAgent>();
    status = GetComponent<EnemyStatus>();
  }

  protected override void DoStart() {
    detectionArea.onSensorEnter += DetectionArea_OnSensorEnter;
    detectionArea.onSensorExit += DetectionArea_OnSensorExit;
    firingArea.onSensorEnter += FiringArea_onSensorEnter;
    firingArea.onSensorExit += FiringArea_onSensorExit;
    agent.speed = status.MoveSpeed;
    agent.angularSpeed = status.RotationSpeed;
    agent.updatePosition = false;
    agent.updateRotation = false;

    BuildBehaviourTree();
  }

  protected override void DoUpdate() {
    if (target) {
      agent.destination = target.transform.position;
    }
    animator.SetBool("isRun", false);
    behaviourTree.Update();
  }

  protected override void DoDied() {
    Destroy(detectionArea);
    Destroy(firingArea);
    Destroy(agent);
    Destroy(gun);
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
    gun.Fire();
  }

  private void Wait() {
  }

  private void ApproachToTarget() {
    agent.destination = target.transform.position;
    transform.position += agent.desiredVelocity * Time.deltaTime;
    agent.nextPosition = transform.position;
    animator.SetBool("isRun", true);
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
      agent.avoidancePriority = 1;
    }
  }

  private void FiringArea_onSensorExit(Collider other) {
    if (IsPlayer(other.gameObject)) {
      agent.nextPosition = transform.position;
      isTargetInRange = false;
      agent.avoidancePriority = 50;
    }
  }

  private bool IsPlayer(GameObject other) {
    var playerLayer = LayerMask.NameToLayer("UnityChan");
    if (other.gameObject.layer == playerLayer) return true;
    return false;
  }

  private bool CanSeeTarget() {
    if (target == null) return false;
    if (!isFighting) return false;
    NavMeshHit hitInfo;
    if (agent.Raycast(target.transform.position, out hitInfo)) return false;
    return true;
  }

  private void JumpOff() {
    if (isJumping) return;
    ApproachToTarget();
    if (agent.isOnOffMeshLink) {
      isFighting = true;
    }
  }

  private void BuildBehaviourTree() {
    var chaseAction = new ActionNode(Chase);
    var canChaseDecorator = new DecoratorNode(chaseAction, () => remainingTimeToChase > 0.0f);
    var approachAction = new ActionNode(ApproachToTarget);
    var canApproachDecorator = new DecoratorNode(approachAction, () => isTargetInDetectionArea);
    var selector1 = new SelectorNode(canApproachDecorator, canChaseDecorator);

    var fireAction = new ActionNode(Fire);
    var canFireDecorator = new DecoratorNode(fireAction, () => { return gun.CanFire(); });
    var faceToTargetAction = new ActionNode(FaceToTarget);
    var selector2 = new SelectorNode(canFireDecorator, faceToTargetAction);

    var canSeeTargetDecorator = new DecoratorNode(selector2, CanSeeTarget);
    var selector3 = new SelectorNode(canSeeTargetDecorator, approachAction);

    var isTargetInRangeDecorator = new DecoratorNode(selector3, () => isTargetInRange);
    var selector4 = new SelectorNode(isTargetInRangeDecorator, selector1);

    var jumpOffAction = new ActionNode(JumpOff);
    var isFightingDecorator = new DecoratorNode(jumpOffAction, () => !isFighting);
    var selector7 = new SelectorNode(isFightingDecorator, selector4);

    var isTargetNullDecorator = new DecoratorNode(selector7, () => target != null);
    var waitAction = new ActionNode(Wait);
    var selector5 = new SelectorNode(isTargetNullDecorator, waitAction);

    var blownChecker = new DecoratorNode(waitAction, () => core.isBlown);
    var selector6 = new SelectorNode(blownChecker, selector5);

    behaviourTree = new BehaviourTree(selector6);
  }

}
