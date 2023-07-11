using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class APiece
{
    public List<Transform> sockets = new List<Transform>();
    public APiece parent;
    public GameObject thisLevelPiece;
    public bool bIsSquare;
    int getSocketTries = 10;

    public void NewPiece(APiece _parent, GameObject _levelPiece)
    {

        parent = _parent;
        thisLevelPiece = _levelPiece;


    }

    public void AddSockets()
    {
        foreach (Transform SocketTransform in thisLevelPiece.transform)
        {
            if (SocketTransform.CompareTag("Socket"))
            {
                sockets.Add(SocketTransform);
            }
        }
    }

    public void RemoveInvalidSocket(Transform socketLocation)
    {

        List<Transform> socketsToRemove = new List<Transform>();

        foreach (Transform socket in sockets)
        {
            if (socketLocation.position == socket.position)
            {
                socketsToRemove.Add(socket);
                Debug.Log("Socket to be removed: " + socket.name);
            }
        }

        foreach (Transform socketToRemove in socketsToRemove)
        {
            sockets.Remove(socketToRemove);
            Debug.Log("Socket Removed: " + socketToRemove.name);
        }
    }
    public Transform GetAttachmentSocket()
    {
        int indexToReturn = GenerateRandomIndex();
        return sockets[indexToReturn];
    }

    public Transform GetSpawnSocket()
    {
        int indexToReturn = GenerateRandomIndex();

        if (indexToReturn != -1 && getSocketTries > 0)
        {

            if (Physics.OverlapSphere(sockets[indexToReturn].position, 3).Length > 1)
            {
                getSocketTries--;
                return GetSpawnSocket();
            }
            getSocketTries = 10;
            return sockets[indexToReturn];
        }
        getSocketTries = 10;
        return null;

    }

    private int GenerateRandomIndex()
    {
        return Random.Range(0, sockets.Count);
    }
}

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
