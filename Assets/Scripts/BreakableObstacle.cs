using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacle : Obstacle
{
 protected override void OnCollisionEnter(Collision other) {
    base.OnCollisionEnter(other);
    Destroy(this.gameObject);
 }
}
