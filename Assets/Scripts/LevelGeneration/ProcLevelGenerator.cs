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
            LevelPiece pieceController = levelPiece.GetComponent<LevelPiece>();
            pieceController.AddSockets();
            piecesInLevel.Add(levelPiece);
            pieceController.parent = currentSpawn.GetComponentInParent<LevelPiece>();
            if (!pieceController.parent)
            {
                currentSpawn = pieceController.GetSpawnSocket();
                continue;
            }
            if (!pieceController.bIsSquare)
            {
                // what if the piece is not square
                //rotate piece si it faces the right direction
                //move piece so it borders the previous one
            }
            else
            {
                Vector3 direction = levelPiece.transform.position - pieceController.parent.gameObject.transform.position;
                levelPiece.transform.position += direction;
            }


            levelPiece.transform.parent = pieceController.parent.transform;

            pieceController.RemoveInvalidSocket(currentSpawn);

            currentSpawn = pieceController.GetSpawnSocket();
            if (currentSpawn == null)
            {
                currentSpawn = CheckForValidSpawnPoint(pieceController);
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
