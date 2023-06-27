using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Hole : MonoBehaviour
{
//Use events for this?

private void OnTriggerEnter(Collider other) {
    if(other.gameObject.tag == "Ball"){
        other.gameObject.SetActive(false);
    }
}
}
