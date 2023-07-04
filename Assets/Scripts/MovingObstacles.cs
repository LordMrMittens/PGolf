using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : Obstacle
{
    GameObject stage;
    [SerializeField] float movingspeed;
    [SerializeField] List<Vector3> targetDestinations = new List<Vector3>();
    int minNumberOfDestinations = 2;
    [SerializeField] int maxNumberOfDestinations;
    [SerializeField] LayerMask obstacleLayer;
      private int currentDestinationIndex = 0;
    void Start()
    {
        stage = GameObject.FindGameObjectWithTag("Ground");
        targetDestinations.Add(this.transform.position);
        int numberOfDestinations = Random.Range(minNumberOfDestinations, maxNumberOfDestinations+1);
        for (int i = 0; i < numberOfDestinations; i++)
        {
            targetDestinations.Add(GetRandomPosition());
        }
        
    }

    private Vector3 GetRandomPosition()
    {
        Bounds levelBounds = stage.GetComponent<Collider>().bounds;
        float RandomX = Random.Range(levelBounds.min.x, levelBounds.max.x);
        float RandomZ = Random.Range(levelBounds.min.z, levelBounds.max.z);
        Vector3 randomLocation = new Vector3(RandomX, 0, RandomZ);
        float distance = Vector3.Distance(transform.position, randomLocation);
        if (Physics.Raycast(transform.position, randomLocation, distance, obstacleLayer))
        {
            return GetRandomPosition();
        }
        return randomLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != targetDestinations[currentDestinationIndex])
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDestinations[currentDestinationIndex], movingspeed * Time.deltaTime);
        }
        else
        {
            currentDestinationIndex = (currentDestinationIndex + 1) % targetDestinations.Count;
        }
    }
}
