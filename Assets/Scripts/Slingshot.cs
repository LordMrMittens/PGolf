using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private Vector3 initialLocation;
    private float pullDistance = 0;
    [SerializeField] float forceMultiplier = 1;
    Camera mainCamera;
    GameObject selectedObject;
    CameraController cameraController;
    [SerializeField] LayerMask balls;
    [SerializeField] LayerMask floor;
    [SerializeField] GameObject SlingshotPrefab;
    GameObject Sling;

    [SerializeField]float dragSpeed = 5f;
    [SerializeField] float MaxPullDistance = 10f;
    
    void Start()
    {
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();
        GameManager.Instance.ResetGame();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50, balls))
            {
                Ball selectedBall = hit.collider.gameObject.GetComponent<Ball>();
                if (selectedBall != null && !selectedBall.isMoving)
                {
                    selectedObject = hit.collider.gameObject;
                    initialLocation = selectedObject.transform.position;
                    GameObject Slingshot = Instantiate(SlingshotPrefab, initialLocation, Quaternion.identity);
                    Sling = Slingshot;
                    selectedBall.RandomiseBallColour();
                }
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (selectedObject != null)
            {
                pullDistance = Vector3.Distance(selectedObject.transform.position, initialLocation);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 50, floor))
                {

                    Vector3 dragPosition = hit.point;
                    dragPosition.y = selectedObject.transform.position.y; // this should be half the selected object so it doesnt go into the ground
                    var step = dragSpeed * Time.deltaTime; // calculate distance to move towards cursor
                    if (pullDistance * forceMultiplier <= MaxPullDistance)
                    {
                        selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, dragPosition, step);
                    }
                    else
                    {
                        selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position,initialLocation, step);
                    }
                    Sling.transform.LookAt(selectedObject.transform, Vector3.up);
                }

                GameManager.Instance.DisplayPowerLevel(pullDistance * forceMultiplier, true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject && selectedObject.GetComponent<Rigidbody>())
            {
                Rigidbody BallRB = selectedObject.GetComponent<Rigidbody>();
                Vector3 direction = (initialLocation - selectedObject.transform.position);
                BallRB.AddForce(direction * (pullDistance * forceMultiplier), ForceMode.Impulse);
                cameraController.SetShotDirection(direction);
                cameraController.objectToFollowRB = BallRB;
                Destroy(Sling);
                GameManager.Instance.AddPointsToScore(1);
            }
            else
            {
                Debug.LogError("ball has no rigidbody");
            }

            selectedObject = null;
            pullDistance = 0;
            GameManager.Instance.DisplayPowerLevel(0, false);
        }
    }
}
