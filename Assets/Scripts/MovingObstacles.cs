using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    [SerializeField] float movingspeed;
    [SerializeField] List<Vector3> targetDestinations = new List<Vector3>();
      private int currentDestinationIndex = 0;
    void Start()
    {
        targetDestinations.Add(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != targetDestinations[currentDestinationIndex])
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDestinations[currentDestinationIndex], movingspeed * Time.deltaTime);
        }
        else
        {
            currentDestinationIndex = (currentDestinationIndex + 1) % targetDestinations.Count;
        }
    }
}
