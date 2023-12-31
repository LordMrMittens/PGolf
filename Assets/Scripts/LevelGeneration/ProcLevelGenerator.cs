using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{

    public class ProcLevelGenerator : MonoBehaviour
    {
        [SerializeField] GameObject[] levelPiecesPrefabs;
        [SerializeField] int numberOfPieces;
        [SerializeField] float wallVerticallOffset = .5f;
        Transform originPoint;
        List<GameObject> piecesInLevel = new List<GameObject>();

        public GameObject startPiece;
        public GameObject endPiece;
        // Start is called before the first frame update
        public void GenerateLevel()
        {
            originPoint = GameObject.Find("Origin").transform;
            Transform currentSpawn = originPoint;
            
            APiece previousPiece = null;
            for (int i = 0; i < numberOfPieces; i++)
            {
                if (currentSpawn == null)
                {
                    return;
                }
                int pieceInt = Random.Range(0, levelPiecesPrefabs.Length);
                GameObject levelPiece = Instantiate(levelPiecesPrefabs[pieceInt], currentSpawn.position, Quaternion.identity);
                APiece piece = new APiece();
                piece.NewPiece(previousPiece, levelPiece);
                previousPiece = piece;
                piece.AddSockets();
                piece.bIsSquare = piece.sockets.Count == 4;
                piecesInLevel.Add(levelPiece);
                if (piece.parent == null)
                {
                    startPiece = levelPiece;
                    currentSpawn = piece.GetSpawnSocket();
                    continue;
                }
                if (!piece.bIsSquare)
                {
                    // what if the piece is not square
                    //rotate piece si it faces the right direction
                    //move piece so it borders the previous one
                }
                else
                {
                    Vector3 direction = levelPiece.transform.position - piece.parent.levelPieceObject.transform.position;
                    levelPiece.transform.position += direction;
                }
               // levelPiece.transform.parent = piece.parent.levelPieceObject.transform;
                piece.RemoveInvalidSocket(currentSpawn);
                currentSpawn = piece.GetSpawnSocket();
                if (currentSpawn == null)
                {
                    currentSpawn = CheckForValidSpawnPoint(piece);
                }
                endPiece = levelPiece;
                //continue
            }
            VerifyPositions();
            GenerateWalls();
        }

        private static Transform CheckForValidSpawnPoint(APiece piece)
        {

            Transform newSpawn = piece.parent.GetSpawnSocket();
            if (newSpawn == null)
            {
                return CheckForValidSpawnPoint(piece.parent);
            }
            return newSpawn;
        }
        private void GenerateWalls()
        {
            DestroyInvalidSockets();
            GameObject[] remainingSockets = GameObject.FindGameObjectsWithTag("Socket");
            for (int i = 0; i < remainingSockets.Length; i++)
            {
                GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
               // wall.AddComponent<MeshFilter>();
               // wall.AddComponent<MeshRenderer>();
               // wall.AddComponent<BoxCollider>();
                Vector3 spawnPos = new Vector3(remainingSockets[i].transform.position.x, remainingSockets[i].transform.position.y + wallVerticallOffset, remainingSockets[i].transform.position.z);
                wall.transform.position = spawnPos;
                wall.transform.rotation = remainingSockets[i].transform.rotation;
                wall.transform.localScale = new Vector3(11, 1, 1);
                wall.transform.parent = remainingSockets[i].transform;
                Obstacle wallObstacle = wall.AddComponent<Obstacle>();
                wallObstacle.SetBumberForce(1);
                wall.tag = "Obstacle";
                wall.layer = LayerMask.NameToLayer("Obstacles");
            }
        }

        private static void DestroyInvalidSockets()
        {
            GameObject[] allSockets = GameObject.FindGameObjectsWithTag("Socket");
            List<GameObject> invalidSockets = new List<GameObject>();
            for (int i = 0; i < allSockets.Length; i++)
            {
                for (int j = 0; j < allSockets.Length; j++)
                {
                    if (allSockets[i] != allSockets[j])
                    {
                        if (Vector3.Distance(allSockets[i].transform.position, allSockets[j].transform.position) < 1f)
                        {
                            invalidSockets.Add(allSockets[i]);
                            invalidSockets.Add(allSockets[i]);
                        }
                    }
                }
            }
            for (int i = 0; i < invalidSockets.Count; i++)
            {
                DestroyImmediate(invalidSockets[i]);
            }
        }

        void VerifyPositions()
        {
            List<GameObject> piecesToDestroy = new List<GameObject>();
            List<GameObject> piecesToKeep = new List<GameObject>();
            GameObject pieceToDestroy;
            for (int i = 0; i < piecesInLevel.Count; i++)
            {
                for (int j = 0; j < piecesInLevel.Count; j++)
                {
                    float pieceDistance = Vector3.Distance(piecesInLevel[i].transform.position, piecesInLevel[j].transform.position);
                    if (pieceDistance < .1)
                    {
                        if ((piecesInLevel[i] == endPiece || piecesInLevel[i] == startPiece) && piecesInLevel[i] != piecesInLevel[j])
                        {
                            if (!piecesToKeep.Contains(piecesInLevel[j]))
                            {
                                piecesToDestroy.Add(piecesInLevel[j]);
                                piecesToKeep.Add(piecesInLevel[i]);
                            }
                        }
                        else
                        {
                            if (!piecesToKeep.Contains(piecesInLevel[i]))
                            {
                                piecesToKeep.Add(piecesInLevel[i]);
                                piecesToKeep.Add(piecesInLevel[j]);
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < piecesToDestroy.Count; x++)
            {
                pieceToDestroy = piecesToDestroy[x];
                piecesInLevel.Remove(pieceToDestroy);
                Destroy(pieceToDestroy);
                
            }
        }
    }
}