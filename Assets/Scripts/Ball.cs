using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Ball : MonoBehaviour
{
    BreakableObstacleColour BallColour;
    public bool isMoving { get; set; }
    Rigidbody ballRB;
    ParticleSystem particles;
    [SerializeField] float ballStopVelocityOverride = .1f;
 

    private void Start()
    {
        isMoving = true;
        ballRB = GetComponent<Rigidbody>();
        BallColour = BreakableObstacleColour.red;
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if(ballRB.velocity.magnitude <= ballStopVelocityOverride){
            ballRB.velocity = Vector3.zero;
            ballRB.angularVelocity = Vector3.zero;
        }
        isMoving = ballRB.velocity != Vector3.zero;
        
    }

    public void RandomiseBallColour(){
        int indexColour = Random.Range(1, System.Enum.GetValues(typeof(BreakableObstacleColour)).Length+1);
        {
            BallColour = (BreakableObstacleColour)indexColour;
        }
        ParticleSystem.MainModule main = particles.main;
        switch (BallColour)
        {
            case BreakableObstacleColour.red:
                main.startColor = Color.red;
                break;
            case BreakableObstacleColour.blue:
                main.startColor = Color.blue;
                break;
            case BreakableObstacleColour.green:
                main.startColor = Color.green;
                break;
            case BreakableObstacleColour.yellow:
                main.startColor = Color.yellow;
                break;
        }

    }
    public BreakableObstacleColour GetBallColour(){
        return BallColour;
    }

}
