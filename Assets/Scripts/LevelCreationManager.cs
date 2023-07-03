using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreationManager : MonoBehaviour
{
    [SerializeField] GameObject startingPointPrefab;
    [SerializeField] GameObject goalPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject bumperPrefab;
    [SerializeField] GameObject stage;
    [SerializeField] int numberOfObstacles = 4;
    [SerializeField] int numberOfBumpers = 4;
    [SerializeField] float minDistanceFromObstacles = .1f;
    [SerializeField] float minDistanceBetweenStartAndGoal = 1;
    [SerializeField] LayerMask ObstacleLayers;
    Vector3 ballSpawnVerticalOffset = new Vector3(0, .3f, 0);


    private void Awake() {
        SetupGame();
    }
    public void SetupGame()
    {
        SpawnObstacles();
        SpawnBumpers();
        SpawnStartAndGoalPoints();
    }
    public void SpawnObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            float randomRotation;
            Vector3 ObstacleLocation = SetObstacleLocationAndRotation(out randomRotation);
            GameObject obstacle = Instantiate(obstaclePrefab, ObstacleLocation, Quaternion.identity);
            obstacle.transform.Rotate(0, randomRotation, 0);
        }
    }
        public void SpawnBumpers()
    {
        for (int i = 0; i < numberOfBumpers; i++)
        {
            float randomRotation;
            Vector3 bumperLocation = SetObstacleLocationAndRotation(out randomRotation);
            GameObject bumper = Instantiate(bumperPrefab, bumperLocation, Quaternion.identity);
            bumper.transform.Rotate(0, randomRotation, 0);
        }
    }

    Vector3 SetObstacleLocationAndRotation(out float randomRotation)
    {
        randomRotation = Random.Range(0f, 360f);
        return GenerateRandomPointInLevel();
    }

    private void SpawnStartAndGoalPoints()
    {
        Vector3 startingPoint = Vector3.zero;
        Vector3 goalPoint = Vector3.zero;
        do
        {
            startingPoint = GenerateRandomPointInLevel();
            goalPoint = GenerateRandomPointInLevel();
        } while (Vector3.Distance(startingPoint, goalPoint) < minDistanceBetweenStartAndGoal);
        Instantiate(startingPointPrefab, startingPoint, Quaternion.identity);
        Instantiate(ballPrefab, startingPoint + ballSpawnVerticalOffset, Quaternion.identity);
        Instantiate(goalPrefab, goalPoint, Quaternion.identity);
        Camera.main.GetComponent<CameraController>().ResetCameraPosition(startingPoint, goalPoint);
    }

    Vector3 GenerateRandomPointInLevel()
    {
        Bounds levelBounds = stage.GetComponent<Collider>().bounds;
        float RandomX = Random.Range(levelBounds.min.x, levelBounds.max.x);
        float RandomZ = Random.Range(levelBounds.min.z, levelBounds.max.z);
        Vector3 randomLocation = new Vector3(RandomX, 0, RandomZ);

        if (Physics.OverlapSphere(randomLocation, minDistanceFromObstacles, ObstacleLayers).Length > 0)
        {
            return GenerateRandomPointInLevel();
        }
        return randomLocation;
    }
}
