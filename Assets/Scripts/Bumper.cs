using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] float bumperForce;
    private void OnCollisionEnter(Collision other)
    {
        Vector3 averageReflectionDir = Vector3.zero;
        int contactCount = other.contacts.Length;
        for (int i = 0; i < contactCount; i++)
        {
            Vector3 reflectedDir = Vector3.Reflect(other.relativeVelocity.normalized, other.contacts[i].normal);
            reflectedDir = Vector3.ProjectOnPlane(reflectedDir, Vector3.up);
            averageReflectionDir += reflectedDir;
        }
        averageReflectionDir /= contactCount;
        Rigidbody ballRB = other.gameObject.GetComponent<Rigidbody>();
        if (ballRB)
        {
            ballRB.AddForce(averageReflectionDir * bumperForce, ForceMode.Impulse);
        }
    }
}
