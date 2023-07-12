using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcLevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] levelPieces;
    [SerializeField] int numberOfPieces;
    [SerializeField] float wallVerticallOffset = .5f;
    Transform originPoint;
    // Start is called before the first frame update
    void Start()
    {
        originPoint = GameObject.Find("Origin").transform;
        Transform currentSpawn = originPoint;
        List<GameObject> piecesInLevel = new List<GameObject>();
        APiece previousPiece = null;
        for (int i = 0; i < numberOfPieces; i++)
        {
            if (currentSpawn == null)
            {
                return;
            }
            int pieceInt = Random.Range(0, levelPieces.Length);
            GameObject levelPiece = Instantiate(levelPieces[pieceInt], currentSpawn.position, Quaternion.identity);
            APiece piece = new APiece();
            piece.NewPiece(previousPiece, levelPiece);
            previousPiece = piece;
            piece.AddSockets();
            piece.bIsSquare = piece.sockets.Count == 4;
            piecesInLevel.Add(levelPiece);
            if (piece.parent == null)
            {
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


            levelPiece.transform.parent = piece.parent.levelPieceObject.transform;

            piece.RemoveInvalidSocket(currentSpawn);

            currentSpawn = piece.GetSpawnSocket();
            if (currentSpawn == null)
            {
                currentSpawn = CheckForValidSpawnPoint(piece);
            }
            //continue
        }

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
            wall.AddComponent<MeshFilter>();
            wall.AddComponent<MeshRenderer>();
            wall.AddComponent<BoxCollider>();
            Vector3 spawnPos = new Vector3(remainingSockets[i].transform.position.x, remainingSockets[i].transform.position.y + wallVerticallOffset, remainingSockets[i].transform.position.z);
            wall.transform.position = spawnPos;
            wall.transform.rotation = remainingSockets[i].transform.rotation;
            wall.transform.localScale = new Vector3(11, 1, 1);
            wall.transform.parent = remainingSockets[i].transform;
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
}
