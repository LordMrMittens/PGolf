using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiece : MonoBehaviour
{
    public List<Transform> sockets = new List<Transform>();
    public LevelPiece parent;

    public bool bIsSquare;
    
    int getSocketTries = 10;

    public void AddSockets()
    {
        foreach (Transform SocketTransform in this.transform)
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
    public Transform GetAttachmentSocket(){
        int indexToReturn = GenerateRandomIndex();
        return sockets[indexToReturn];
    }

    public Transform GetSpawnSocket()
    {
        int indexToReturn = GenerateRandomIndex();

        if (indexToReturn != -1 && getSocketTries >0)
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
