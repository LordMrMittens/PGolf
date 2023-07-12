using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LevelGeneration
{

    public class LevelCreationManager : MonoBehaviour
    {
        [SerializeField] GameObject startingPointPrefab;
        [SerializeField] GameObject goalPrefab;
        [SerializeField] GameObject ballPrefab;
        [SerializeField] GameObject[] obstaclePrefabs;
        [SerializeField] GameObject bumperPrefab;
        GameObject stage;
        [SerializeField] int numberOfObstacles = 4;
        [SerializeField] int numberOfBumpers = 4;
        [SerializeField] float minDistanceFromObstacles = .1f;
        [SerializeField] float minDistanceBetweenStartAndGoal = 1;
        [SerializeField] LayerMask ObstacleLayers;
        Vector3 ballSpawnVerticalOffset = new Vector3(0, .3f, 0);

        ProcLevelGenerator levelGenerator;


        private void Awake()
        {
            levelGenerator = GetComponent<ProcLevelGenerator>();
            SetupGame();
        }
        public void SetupGame()
        {
            levelGenerator.GenerateLevel();
            SpawnObstacles();
            SpawnBumpers();
            SpawnStartAndGoalPoints();
        }


        public void SpawnObstacles()
        {
            for (int i = 0; i < numberOfObstacles; i++)
            {
                int randomObstaclePrefabIndex = Random.Range(0, obstaclePrefabs.Length);
                float randomRotation;
                Vector3 ObstacleLocation = SetObstacleLocationAndRotation(out randomRotation);
                GameObject obstacle = Instantiate(obstaclePrefabs[randomObstaclePrefabIndex], ObstacleLocation, Quaternion.identity);
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
            Bounds levelBounds = GetLevelBounds();
            float RandomX = Random.Range(levelBounds.min.x, levelBounds.max.x);
            float RandomZ = Random.Range(levelBounds.min.z, levelBounds.max.z);
            Vector3 randomLocation = new Vector3(RandomX, 0, RandomZ);
            if (Physics.OverlapSphere(randomLocation, minDistanceFromObstacles, ObstacleLayers).Length > 0)
            {
                return GenerateRandomPointInLevel();
            }
            return randomLocation;
        }
private Bounds GetLevelBounds()
        {
            float maxX = Mathf.Infinity; ;
            float maxY = Mathf.Infinity; ;
            float maxZ = Mathf.Infinity;
            float minX = Mathf.Infinity;
            float minY = Mathf.Infinity;
            float minZ = Mathf.Infinity;
            GameObject[] levelPieces = GameObject.FindGameObjectsWithTag("LevelPiece");
            foreach (GameObject collider in levelPieces)
            {
                
                if (maxX > collider.transform.position.x || maxX == Mathf.Infinity)
                {
                    maxX = collider.transform.position.x + 5;
                }
                if (maxY > collider.transform.position.y || maxY == Mathf.Infinity)
                {
                    maxY = collider.transform.position.y + 5;
                }
                if (maxZ > collider.transform.position.z || maxZ == Mathf.Infinity)
                {
                    maxZ = collider.transform.position.z+ 5;
                }
                if (minX < collider.transform.position.x|| minX == Mathf.Infinity)
                {
                    minX = collider.transform.position.x -5;
                }
                if (minY < collider.transform.position.y|| minY == Mathf.Infinity)
                {
                    minY = collider.transform.position.y -5;
                }
                if (minZ < collider.transform.position.z|| minZ == Mathf.Infinity)
                {
                    minZ = collider.transform.position.z -5;
                }
            }
           
            Bounds bounds = new Bounds();
            bounds.min = new Vector3(minX, minY, minZ);
            bounds.max = new Vector3(maxX, maxY, maxZ);
            Debug.Log(bounds);
            return bounds;

        }
    }
}