using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Ball : MonoBehaviour
{
    public bool isMoving { get; set; }
    Rigidbody ballRB;

    [SerializeField] float ballStopVelocityOverride = .1f;
 

    private void Start()
    {
        isMoving = true;
        ballRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(ballRB.velocity.magnitude <= ballStopVelocityOverride){
            ballRB.velocity = Vector3.zero;
            ballRB.angularVelocity = Vector3.zero;
        }
        isMoving = ballRB.velocity != Vector3.zero;

    }

}
