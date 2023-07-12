using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LevelGeneration
{
    class APiece
    {
        public List<Transform> sockets = new List<Transform>();
        public APiece parent;
        public GameObject levelPieceObject;
        public bool bIsSquare;
        int getSocketTries = 10;

        public void NewPiece(APiece _parent, GameObject _levelPiece)
        {
            parent = _parent;
            levelPieceObject = _levelPiece;
        }
        public void AddSockets()
        {
            int index = 0;
            sockets.Clear();
            foreach (Transform SocketTransform in levelPieceObject.transform)
            {
                if (SocketTransform.CompareTag("Socket"))
                {
                    sockets.Add(SocketTransform);
                    index++;
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
                }
            }

            foreach (Transform socketToRemove in socketsToRemove)
            {
                sockets.Remove(socketToRemove);
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

        public List<Transform> GetWallSockets()
        {

            AddSockets();
            List<Transform> socketsToRemove = new List<Transform>();
            foreach (Transform socket in sockets)
            {
                if (Physics.OverlapSphere(socket.position, .5f).Length > 1)
                {
                    socketsToRemove.Add(socket);
                }
            }
            foreach (Transform socketToRemove in socketsToRemove)
            {
                sockets.Remove(socketToRemove);
            }
            return sockets;
        }
    }
}