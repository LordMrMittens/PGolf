using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{

    [SerializeField] Vector3 rotationSpeed= Vector3.up;


    // Update is called once per frame
    void Update()
    {
       this.transform.Rotate(rotationSpeed, Space.Self);
    }
}
