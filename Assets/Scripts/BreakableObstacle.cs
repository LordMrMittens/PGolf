using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BreakableObstacleColour
{
    red = 1, 
    blue = 2, 
    green = 3, 
    yellow =4
}
public class BreakableObstacle : Obstacle
{
    [SerializeField] Material redMaterial;
    
    [SerializeField] Material blueMaterial;
    
    [SerializeField] Material greenMaterial;
    
    [SerializeField] Material yellowMaterial;
    BreakableObstacleColour obstacleColour;


    private void Start() {
        RandomiseObstacleColour();
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        Ball ball = other.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            if (ball.GetBallColour() == obstacleColour)
            {
                GameManager.Instance.AddPointsToScore(-1);
                Destroy(this.gameObject);
            }
        }

    }

    private void RandomiseObstacleColour(){
                int indexColour = Random.Range(1, System.Enum.GetValues(typeof(BreakableObstacleColour)).Length+1);
        {
            obstacleColour = (BreakableObstacleColour)indexColour;
        }
            Renderer obstacleRenderer = GetComponent<Renderer>();
        switch (obstacleColour)
        {
            case BreakableObstacleColour.red:
                obstacleRenderer.material = redMaterial;
                break;
            case BreakableObstacleColour.blue:
                obstacleRenderer.material = blueMaterial;
                break;
            case BreakableObstacleColour.green:
                obstacleRenderer.material = greenMaterial;
                break;
            case BreakableObstacleColour.yellow:
                obstacleRenderer.material = yellowMaterial;
                break;
        }
    }
}
