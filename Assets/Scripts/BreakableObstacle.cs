using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BreakableObstacleColour
{
    red = 1, 
    blue = 2, 
    green = 3, 
    yellow =4
}
public class BreakableObstacle : Obstacle
{

BreakableObstacleColour obstacleColour;
 protected override void OnCollisionEnter(Collision other) {
    base.OnCollisionEnter(other);
    Destroy(this.gameObject);
 }
}
