using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//First Attach this the player object (you will also need a Character Controller)
//Then Assign the main camera to the player camera below

[RequiredComponent(typeof(characterController))]

public class FirstPersonController : MonoBehaviour
{
    //our walking / running speeds
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;

    //Do we want the user to jump?
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    //we need a reference to the camera obj
    public Camera camera;

    //we want to limit how fast the user can look around
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f; // we dont want the user to be able to look too far up

    //we need a char controller
    CharacterController characterController;

    //we make a V3 to decide where the user is looking
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //the inspector does not need to know this exist
    [HideInInspector]
    public bool canMove = true; //we can turn this off during cutscenes so the player cant move

    void Start()
    {
       characterController = GetComponent<CharacterController>();
       if(characterController == null) //THIS SHOULD NEVER BE NULL
       {
            exit(1); //If this happens we fucked up somewhere
       }

       //we want to lock the curser to the screen while controling the player
       Cursor.lockState = CursorLockMode.Locked;
       //we also dont want the user to see the cursor
       Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.transformDirection(Vector3.forward);
        Vector3 right = transform.transformDirection(Vector3.right);

        //pressing shift will allow the user to run
        bool isRunning = input.GetKey(KeyCode.LeftShift);

        //users mouse sensitivity
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Virtical") : 0;
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Apply Gravity
        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        //a formula for moving the camera with the mouse
        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0)
        }
    }
}