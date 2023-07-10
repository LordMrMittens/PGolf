using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcLevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] levelPieces;
    [SerializeField] int numberOfPieces;
    Transform originPoint;
    // Start is called before the first frame update
    void Start()
    {
        originPoint = GameObject.Find("Origin").transform;
        Transform currentSpawn = originPoint;
        List<GameObject> piecesInLevel = new List<GameObject>();
        for (int i = 0; i < numberOfPieces; i++)
        {
            if (currentSpawn == null)
            {
                return;
            }
            int pieceInt = Random.Range(0, levelPieces.Length);
            GameObject levelPiece = Instantiate(levelPieces[pieceInt], currentSpawn.position, Quaternion.identity);
            LevelPiece piece = levelPiece.AddComponent<LevelPiece>();
            piece.AddSockets();
            piecesInLevel.Add(levelPiece);
            piece.parent = currentSpawn.GetComponentInParent<LevelPiece>();
            if (!piece.parent)
            {
                currentSpawn = piece.GetSpawnSocket();
                continue;
            }

            Vector3 direction = levelPiece.transform.position - piece.parent.gameObject.transform.position;
            levelPiece.transform.position += direction;
            levelPiece.transform.parent = piece.parent.transform;

            piece.RemoveInvalidSocket(currentSpawn);

            currentSpawn = piece.GetSpawnSocket();
            if (currentSpawn == null)
            {
                currentSpawn = CheckForValidSpawnPoint(piece);
            }
            //continue
        }
    }

    private static Transform CheckForValidSpawnPoint(LevelPiece piece)
    {

        Transform newSpawn = piece.parent.GetSpawnSocket();
        if (newSpawn == null)
        {
            return CheckForValidSpawnPoint(piece.parent);
        }
        return newSpawn;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
