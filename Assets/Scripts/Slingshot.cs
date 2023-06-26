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
    void Start()
    {
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50, balls))
            {
                Ball selectedBall = hit.collider.gameObject.GetComponent<Ball>();
                if (selectedBall != null)
                {
                    selectedObject = hit.collider.gameObject;
                    initialLocation = selectedObject.transform.position;
                    
                }
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (selectedObject != null)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 50, floor))
                {

                        Vector3 dragPosition = hit.point;
                        dragPosition.y = selectedObject.transform.position.y; // this should be half the selected object so it doesnt go into the ground
                        selectedObject.transform.position = dragPosition;
                }
                pullDistance = Vector3.Distance(selectedObject.transform.position, initialLocation);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject && selectedObject.GetComponent<Rigidbody>())
            {
                Rigidbody BallRB = selectedObject.GetComponent<Rigidbody>();
                Vector3 direction = (initialLocation - selectedObject.transform.position);
                BallRB.AddForce(direction * (pullDistance * forceMultiplier), ForceMode.Impulse);
                cameraController.objectToFollow = selectedObject.GetComponent<Ball>();
            }
            else
            {
                Debug.LogError("ball has no rigidbody");
            }

            selectedObject = null;
            pullDistance = 0;
        }
    }
}
