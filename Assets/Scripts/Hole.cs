using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hole : MonoBehaviour
{
//Use events for this?

private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {   
            if (ball.isGrabbed == false)
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.EndOfGame();
            }
        }
    }
}
