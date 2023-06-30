using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody objectToFollowRB { get; set; }
    float objectNullTimeout = .2f;
    float objectNullCounter = 0f;
    Vector3 playerPosition;
    Vector3 goalPosition;

    [SerializeField] float horizontalRotationSpeed = 3f;
    [SerializeField] float verticalRotationSpeed = 2f;
    [SerializeField] float verticalOffset;
    [SerializeField] float horizontalOffset=3;
    void LateUpdate()
    {

        if (objectToFollowRB != null)
        {
            objectNullCounter += Time.deltaTime;
            if (objectNullCounter >= objectNullTimeout)
            {
                Vector3 newCameraPos = objectToFollowRB.transform.position;
                newCameraPos.y = this.transform.position.y;
                newCameraPos.z -= horizontalOffset;
                transform.position = newCameraPos;
                if (objectToFollowRB.velocity == Vector3.zero)
                {
                    objectNullCounter =0;
                    playerPosition = objectToFollowRB.transform.position;
                    objectToFollowRB = null;
                }
            }
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            transform.RotateAround(playerPosition, Vector3.up, horizontalInput * horizontalRotationSpeed);

            // Calculate the new camera position after horizontal rotation
            Vector3 newCameraPos = transform.position;
            newCameraPos.y = playerPosition.y + verticalOffset;

            // Tilt the camera vertically based on input
            float verticalAngle = Mathf.Clamp(transform.eulerAngles.x - verticalInput * verticalRotationSpeed, 0f, 60f);
            transform.rotation = Quaternion.Euler(verticalAngle, transform.eulerAngles.y, transform.eulerAngles.z);

            // Set the camera position after vertical tilt
            transform.position = newCameraPos;
        }

    }

    public void ResetCameraPosition(Vector3 target, Vector3 goalTargetPosition)
    {
        Vector3 newCameraPos = target;
        goalPosition = goalTargetPosition;
        playerPosition = target;
        newCameraPos.y = this.transform.position.y;
        newCameraPos.z -= horizontalOffset;
        transform.position = newCameraPos;
        
        
    }
}
