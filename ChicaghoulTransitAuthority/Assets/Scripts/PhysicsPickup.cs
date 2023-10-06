using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this on the player and put the pickup layer on the objects you want to pick up
/// </summary>

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField] private LayerMask pickUpMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform PickupTarget;
    [Space]
    [SerializeField] private float PickupRange;
    private Rigidbody CurrentObject;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(CurrentObject)
            {
                CurrentObject.useGravity = true;
                CurrentObject = null;
            }

            Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(CameraRay, out RaycastHit hitInfo, PickupRange, pickUpMask))
            {
                CurrentObject = hitInfo.rigidbody;
                CurrentObject.useGravity = false;
            }
        }
    }

    void FixedUpdate()
    {
        if(CurrentObject)
        {
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;

            CurrentObject.velocity = DirectionToPoint * 12f * DistanceToPoint;
        }

        if(Input.GetMouseButtonDown(0) && CurrentObject)
        {
            Vector3 DirectionToPoint = PickupTarget.position - CurrentObject.position;
            float DistanceToPoint = DirectionToPoint.magnitude;
            CurrentObject.velocity = DirectionToPoint * -12f * DistanceToPoint;

            CurrentObject.useGravity = true;
            CurrentObject = null;
        }
    }
}
