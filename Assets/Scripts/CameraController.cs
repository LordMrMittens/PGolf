using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody objectToFollowRB {get; set;}
    float objectNullTimeout = .2f;
    float objectNullCounter = 0f;
    [SerializeField] float verticalOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToFollowRB != null)
        {
            objectNullCounter += Time.deltaTime;
            if (objectNullCounter >= objectNullTimeout)
            {
                Vector3 newCameraPos = objectToFollowRB.transform.position;
                newCameraPos.y = this.transform.position.y;
                newCameraPos.z -= 3;
                transform.position = newCameraPos;
                if (objectToFollowRB.velocity == Vector3.zero)
                {
                    objectNullCounter =0;
                    objectToFollowRB = null;
                }
            }
        }

    }
}
