using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody objectToFollowRB { get; set; }
    [SerializeField] private float objectNullTimeout = .2f;
    private float objectNullCounter = 0f;

    [SerializeField] private float horizontalRotationSpeed = 3f;
    [SerializeField] private float verticalRotationSpeed = 2f;
    [SerializeField] private float verticalOffset = 6f;
    [SerializeField] private float horizontalOffset = 3f;

    private float minLookAngle = 0f;
    private float maxLookAngle = 60f;

    private Vector3 playerPosition;
    private Vector3 goalPosition;
    private Vector3 directionOfShot;

    Ball ball;

    

    private void LateUpdate()
    {
        if( ball == null){
            ball = GameObject.FindObjectOfType<Ball>();
        } else {
            if (ball.isMoving && ball.isGrabbed ==false){
                objectToFollowRB = ball.gameObject.GetComponent<Rigidbody>();
            }
        }
        if (objectToFollowRB != null)
        {
            objectNullCounter += Time.deltaTime;
            if (objectNullCounter >= objectNullTimeout)
            {
                Vector3 newCameraPos = objectToFollowRB.transform.position - (directionOfShot * horizontalOffset);
                newCameraPos.y = transform.position.y;
                transform.position = newCameraPos;
                transform.LookAt(objectToFollowRB.transform.position);
                if (objectToFollowRB.velocity == Vector3.zero)
                {
                    objectNullCounter = 0;
                    playerPosition = objectToFollowRB.transform.position;
                    objectToFollowRB = null;
                    ResetCameraPosition(playerPosition, goalPosition);
                }
            }
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            verticalOffset += Input.mouseScrollDelta.y;

            transform.RotateAround(playerPosition, Vector3.up, horizontalInput * horizontalRotationSpeed);

            Vector3 newCameraPos = transform.position;
            newCameraPos.y = playerPosition.y + verticalOffset;

            float verticalAngle = Mathf.Clamp(transform.eulerAngles.x - verticalInput * verticalRotationSpeed, minLookAngle, maxLookAngle);
            transform.rotation = Quaternion.Euler(verticalAngle, transform.eulerAngles.y, transform.eulerAngles.z);

            transform.position = newCameraPos;
        }
    }

    public void ResetCameraPosition(Vector3 target, Vector3 goalTargetPosition)
    {
        goalPosition = goalTargetPosition;
        playerPosition = target;
        Vector3 direction = (goalTargetPosition - target).normalized;
        Vector3 newCameraPos = target - (direction * horizontalOffset);
        newCameraPos.y = transform.position.y;
        transform.position = newCameraPos;
        transform.LookAt(target);
    }

    public void SetShotDirection(Vector3 _directionOfShot)
    {
        directionOfShot = _directionOfShot;
    }
}
