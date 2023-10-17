using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    public static TrainMovement instance;
    public bool hasArrived;
    private enum State { idle, moving, stopping }
    
    [Header("Time Info")]
    [SerializeField] float waitTime;

    [Header("Movement Info")]
    [SerializeField] float DistanceToMove;
    private float distanceMoved;
    [SerializeField] Vector3 Direction;
    [Header("speed")]
    [SerializeField] float Speed;
    [SerializeField] float SlowDownSpeed;
    private float tempSpeed;

    [Header("Audio")]
    [SerializeField]
    AudioSource trainAS;

    private State state = State.idle;

    void Start()
    {
        tempSpeed = Speed;
        instance = this;
    }

    void FixedUpdate()
    {
        if(Time.time == waitTime){state = State.moving;}
        switch(state)
        {
            case State.idle:
                break; 

            case State.moving:
                moving();
                break;

            case State.stopping:
                stopping();
                break;
        }
    }

    void moving()
    {
        if (!trainAS.isPlaying)
        {
            trainAS.Play();
        }
        transform.position += Direction * Speed;
        distanceMoved += Speed;
        if(distanceMoved >= DistanceToMove)
        {
            state = State.stopping;
        }
    }

    void stopping()
    {
        transform.position += Direction * tempSpeed;
        tempSpeed -= SlowDownSpeed;
        if(tempSpeed <= 0)
        {
            state = State.idle;
            hasArrived = true;
        }
    }
}
