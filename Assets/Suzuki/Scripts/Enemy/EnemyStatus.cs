using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {
  [SerializeField]
  private float moveSpeed;
  [SerializeField]
  private float rotationSpeed;
  [SerializeField]
  private float firePower;

  public float MoveSpeed { get { return moveSpeed; } }
  public float RotationSpeed { get { return rotationSpeed; } }
  public float FirePower { get { return firePower; } }

}
