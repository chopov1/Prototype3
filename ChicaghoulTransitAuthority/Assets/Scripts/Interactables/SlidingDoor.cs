using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    enum DoorState { idle, opening, closing }
    DoorState state;

    [SerializeField]
    Vector3 DirectionToOpen;

    [SerializeField]
    float openSpeed;

    [SerializeField]
    float distanceToMove;

    float distanceMoved;

    // Start is called before the first frame update
    void Start()
    {
        distanceMoved = 0;
        state = DoorState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DoorState.idle:
                break;
            case DoorState.opening:
                opening();
                break;
            case DoorState.closing:
                closing();
                break;

        }
    }

    void opening()
    {
        transform.position += DirectionToOpen * openSpeed;
        distanceMoved+= openSpeed;
        if (distanceMoved >= distanceToMove)
        {
            state = DoorState.idle;
            Debug.Log(transform.position);
        }
    }

    void closing()
    {
        transform.position -= DirectionToOpen * openSpeed;
        distanceMoved-= openSpeed;
        if (distanceMoved <= 0)
        {
            state = DoorState.idle;
            Debug.Log(transform.position);
        }
    }

    public void OpenDoor()
    {
        if(state == DoorState.idle)
        {
            if (distanceMoved <= 0)
            {
                state = DoorState.opening;
            }
            else
            {
                CloseDoor();
            }
        }
    }

    public void CloseDoor()
    {
        if(state == DoorState.idle)
        {
            if (distanceMoved >= distanceToMove)
            {
                state = DoorState.closing;
            }
            else
            {
                OpenDoor();
            }
        }
    }

}
