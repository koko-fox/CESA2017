using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {
  [SerializeField]
  private float moveSpeed;
  [SerializeField]
  private float rotationSpeed;

  public float MoveSpeed { get { return moveSpeed; } }
  public float RotationSpeed { get { return rotationSpeed; } }

}
