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

    public void SetBallColour(BreakableObstacleColour colour){
        ParticleSystem.MainModule main = particles.main;
        switch (colour)
        {
            case BreakableObstacleColour.red:
                main.startColor = Color.red;
                particles.gameObject.SetActive(true);
                break;
            case BreakableObstacleColour.blue:
                main.startColor = Color.blue;
                particles.gameObject.SetActive(true);
                break;
            case BreakableObstacleColour.green:
                main.startColor = Color.green;
                particles.gameObject.SetActive(true);
                break;
            case BreakableObstacleColour.yellow:
                main.startColor = Color.yellow;
                particles.gameObject.SetActive(true);
                break;
            default :
            particles.gameObject.SetActive(false);
            break;
        }

    }
    public BreakableObstacleColour GetBallColour(){
        return BallColour;
    }

}
