using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : Obstacle
{
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}
