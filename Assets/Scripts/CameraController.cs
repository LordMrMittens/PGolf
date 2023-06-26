using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Ball objectToFollow {get; set;}
    [SerializeField] float verticalOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToFollow != null)
        {
            Vector3 newCameraPos = objectToFollow.transform.position;
            newCameraPos.y = this.transform.position.y;
            newCameraPos.z -= 3;
            transform.position = newCameraPos;
            if (!objectToFollow.isMoving)
            {
                objectToFollow = null;
            }
        }

    }
}
