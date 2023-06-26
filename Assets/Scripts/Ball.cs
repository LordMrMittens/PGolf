using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isMoving { get; set; }
    Rigidbody ballRB;

    private void Start()
    {
        isMoving = true;
        ballRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {


    }
}
