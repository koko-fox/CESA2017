using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGun : MonoBehaviour {
  public abstract void Fire();
  public abstract bool CanFire();

}
