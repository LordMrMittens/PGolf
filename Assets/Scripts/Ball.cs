using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Ball : MonoBehaviour
{
    BreakableObstacleColour BallColour;
    public bool isMoving { get; private set; }
    public bool isGrabbed{ get; private set; } = false;
    Rigidbody ballRB;
    ParticleSystem particles;
    [SerializeField] float ballStopVelocityOverride = .1f;
    [SerializeField] int numberOfShotsWithPowerUp = 2;
    int remainingPowerUpShots;
 

    private void Start()
    {
        isMoving = true;
        ballRB = GetComponent<Rigidbody>();
        BallColour = BreakableObstacleColour.red;
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (ballRB.velocity.magnitude <= ballStopVelocityOverride)
        {
            ballRB.velocity = Vector3.zero;
            ballRB.angularVelocity = Vector3.zero;
            if (remainingPowerUpShots <= 0)
            {
                SetBallColour(BreakableObstacleColour.none);
            }
        }
        isMoving = ballRB.velocity != Vector3.zero;

    }
    public void UseUpPowerUpShot()
    {
        if (remainingPowerUpShots > 0)
        {
            remainingPowerUpShots--;
        }
    }

    public void SetBallColour(BreakableObstacleColour colour)
    {
        ParticleSystem.MainModule main = particles.main;
        BallColour = colour;
        switch (BallColour)
        {
            case BreakableObstacleColour.none:
                particles.gameObject.SetActive(false);
                
                break;
            case BreakableObstacleColour.red:
                main.startColor = Color.red;
                particles.gameObject.SetActive(true);
                remainingPowerUpShots = numberOfShotsWithPowerUp;
                break;
            case BreakableObstacleColour.blue:
                main.startColor = Color.blue;
                particles.gameObject.SetActive(true);
                remainingPowerUpShots = numberOfShotsWithPowerUp;
                break;
            case BreakableObstacleColour.green:
                main.startColor = Color.green;
                particles.gameObject.SetActive(true);
                remainingPowerUpShots = numberOfShotsWithPowerUp;
                break;
            case BreakableObstacleColour.yellow:
                main.startColor = Color.yellow;
                particles.gameObject.SetActive(true);
                remainingPowerUpShots = numberOfShotsWithPowerUp;
                break;
            default :
            particles.gameObject.SetActive(false);
            break;
        }

    }
    public BreakableObstacleColour GetBallColour(){
        return BallColour;
    }
    public IEnumerator WaitForBallToStop(){
        yield return new WaitForSeconds (.2f);

        yield return new WaitUntil(() => isMoving == false);
        UseUpPowerUpShot();
    }

    public void SetIsGrabbed(bool _isGrabbed){
        isGrabbed = _isGrabbed;
    }
}
