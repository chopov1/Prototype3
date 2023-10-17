using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool hasPlayedSound;

    [Header("DeBug")]
    public Vector3 speed;
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [SerializeField]
    AudioClip[] footstepSounds;

    [SerializeField]
    float footstepSpeed;

    AudioSource playerAS;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        hasPlayedSound = true;
        playerAS = GetComponent<AudioSource>();
    }

    AudioClip getRandomFootstepSound()
    {
        return footstepSounds[Random.Range(0, footstepSounds.Length-1)];
    }

    void PlayFootstepSound()
    {
        playerAS.PlayOneShot(getRandomFootstepSound());
    }

    private void Update()
    {
        //we want to see it the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.D))
        {
            speed = rb.velocity;
            rb.velocity = Vector3.zero;
        }
        
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        if(horizontalInput + verticalInput != 0)
        {
            if (hasPlayedSound)
            {
                hasPlayedSound = false;
                StartCoroutine(waitForStep());
                PlayFootstepSound();
            }
        }
    }

    IEnumerator waitForStep()
    {
        yield return new WaitForSeconds(footstepSpeed);
        hasPlayedSound = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit the player velocity
        if(flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
