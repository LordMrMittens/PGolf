using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [field : SerializeField] public int par { get; set; } = 2;
    public int currentScore { get; private set; } = 0;
    [SerializeField] UIManager uIManager;
    [SerializeField] GameObject startingPointPrefab;
    [SerializeField] GameObject goalPrefab;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject stage;  
    [SerializeField] float minDistanceFromObstacles = .1f;
    [SerializeField] float minDistanceBetweenStartAndGoal = 1;
    [SerializeField] LayerMask ObstacleLayers;
    Vector3 ballSpawnVerticalOffset = new Vector3(0,.3f,0);
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPointsToScore(int points)
    {
        currentScore += points;
    }

    public void EndOfGame()
    {
        uIManager.DisplayEndOfGameScore(par,currentScore);
    }
    public void DisplayPowerLevel(float powerLevel, bool shouldPanelBeOn){
        uIManager.UpdatePowerLevel(powerLevel);
        uIManager.TogglePowerLevelPanel(shouldPanelBeOn);
    }

    public void ResetGame()
    {
        currentScore = 0;
        DisplayPowerLevel(0, false);
        Vector3 startingPoint = Vector3.zero;
        Vector3 goalPoint = Vector3.zero;
        do
        {
            startingPoint = GenerateRandomPointInLevel();
            goalPoint = GenerateRandomPointInLevel();
        } while (Vector3.Distance(startingPoint, goalPoint) < minDistanceBetweenStartAndGoal);
        Instantiate(startingPointPrefab, startingPoint, Quaternion.identity);
        Instantiate(ballPrefab, startingPoint+ballSpawnVerticalOffset, Quaternion.identity);
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
