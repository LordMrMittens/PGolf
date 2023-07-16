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
        [SerializeField] GameObject[] breakableObstaclePrefabs;
        [SerializeField] GameObject[] movingObstaclePrefabs;
        [SerializeField] GameObject[] spinningObstaclePrefabs;
        [SerializeField] GameObject[] bumperPrefabs;
        [SerializeField] GameObject[] powerNodePrefab;
        GameObject stage;
        [SerializeField] public int numberOfObstacles = 4;
        [SerializeField] public int numberOfMovingObstacles = 3;
        [SerializeField] public int numberOfBreakableObstacles = 3;
        [SerializeField] public int numberOfSpinningObstacles = 3;
        [SerializeField] public int numberOfBumpers = 4;
        [SerializeField] public int numberOfPowerNodesToSpawn = 4;
        [SerializeField] float minDistanceFromObstacles = .1f;
        [SerializeField] float minDistanceBetweenStartAndGoal = 1;
        [SerializeField] LayerMask ObstacleLayers;
        [SerializeField] LayerMask GroundLayers;
        Vector3 ballSpawnVerticalOffset = new Vector3(0, .3f, 0);

        ProcLevelGenerator levelGenerator;


        private void Awake()
        {
            levelGenerator = GetComponent<ProcLevelGenerator>();
        }
        public void SetupGame()
        {
            levelGenerator.GenerateLevel();
            SpawnLevelObjects(obstaclePrefabs,numberOfObstacles);
            SpawnLevelObjects(bumperPrefabs,numberOfBumpers);
            SpawnLevelObjects(movingObstaclePrefabs,numberOfMovingObstacles);
            SpawnLevelObjects(breakableObstaclePrefabs,numberOfBreakableObstacles);
            SpawnLevelObjects(spinningObstaclePrefabs,numberOfSpinningObstacles);
            SpawnLevelObjects(powerNodePrefab,numberOfPowerNodesToSpawn);
            SpawnStartAndGoalPoints();

        }

        public void SpawnLevelObjects(GameObject[] _objectsToSpawn, int numberToSpawn)
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                int randomObstaclePrefabIndex = Random.Range(0, _objectsToSpawn.Length);
                float randomRotation;
                Vector3 ObstacleLocation = SetObstacleLocationAndRotation(out randomRotation);
                GameObject obstacle = Instantiate(_objectsToSpawn[randomObstaclePrefabIndex], ObstacleLocation, Quaternion.identity);
                obstacle.transform.Rotate(0, randomRotation, 0);
            }
        }

        Vector3 SetObstacleLocationAndRotation(out float randomRotation)
        {
            float[] availableRotations = new float[3] {0f,45f,90f};
            randomRotation = availableRotations[Random.Range(0, availableRotations.Length)];
            return GenerateRandomPointInLevel();
        }

        private void SpawnStartAndGoalPoints()
        {
            Vector3 startingPoint = levelGenerator.startPiece.transform.position;
            Vector3 goalPoint = levelGenerator.endPiece.transform.position;
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
            Vector3 randomLocation = new Vector3(RandomX, 50, RandomZ);
            RaycastHit hit;
            if (Physics.Raycast(randomLocation, Vector3.down, out hit, 70, GroundLayers))
            {
                if (!hit.collider.CompareTag("Ground"))
                {
                    return GenerateRandomPointInLevel();
                }
                if (Physics.OverlapSphere(hit.point, minDistanceFromObstacles, ObstacleLayers).Length > 0)
                {
                    return GenerateRandomPointInLevel();
                }
                if( hit.collider.gameObject == levelGenerator.startPiece || hit.collider.gameObject == levelGenerator.endPiece){
                    return GenerateRandomPointInLevel();
                }
                return hit.point;
            }
            return GenerateRandomPointInLevel();
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
            return bounds;

        }
    }
}