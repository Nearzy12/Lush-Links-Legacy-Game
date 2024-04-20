using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player variables
    public float playerSpeed;
    public float sprintSpeed;
    public float slowSpeed;
    public Transform orientation;
    private Vector3 moveDirection;
    private Rigidbody rb;

    // Player Input variables
    public float horizontalInput;
    public float verticalInput;
    public bool jumpInput;
    public bool sprintInput;

    // Add variables for drag, and jumping
    public float playerHeight;
    public bool isGrounded;
    public float dragAmount;
    public LayerMask whatIsGround;

    public float jumpHeight;
    public float jumpCooldown;
    public float airMultiplier;
    public bool allowedToJump;

    //public Transform playerRotation;
    public CharacterController characterController;
    public AnimationStateController animationState;
    private Vector3 playerVelocity;
    public float gravityValue;

    public bool onMachine;


    private float raycastDistance;

    // Start is called before the first frame update
    void Start()
    {
        allowedToJump = true;
        onMachine = false;
    }

    // Update is called once per frame
    void Update()
    {
        RayCastGround();
        GetInput();
        MovePlayer();
    }

    private void GetInput()
    {
        // Get player inputs, then apply that force to the rb.
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetKey(KeyCode.Space);
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        // Jump input
        if (jumpInput && allowedToJump && isGrounded)
        {
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(sprintInput)
        {
            playerSpeed = sprintSpeed;
        }
        else if(sprintInput == false)
        {
            playerSpeed = slowSpeed;
        }


    }

    private void RayCastGround()
    {
        // Cast a ray towards the ground to check if the player is in the air or not
        // If they are on the ground apply drag, if they aren't ont the ground set the drag to zero.
        raycastDistance = 0.2f;
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;  // Small offset from the ground
        isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, raycastDistance, whatIsGround);

        // Debugging ground check
        Debug.DrawRay(raycastOrigin, Vector3.down * raycastDistance, Color.red);
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        //Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Calculate and apply the movement
        //Vector3 moveVector = moveDirection * playerSpeed * Time.deltaTime;

        Vector3 moveVector = transform.right * horizontalInput + transform.forward * verticalInput;
        characterController.Move(moveVector * playerSpeed * Time.deltaTime);

        if (isGrounded)
        {
            // Player is on the ground
            //Debug.Log("Player is on the ground");
        }
        else
        {
            // Player is in the air
            //Debug.Log("Player is in the air");
            ApplyGravity();
        }
    }

    private void Jump()
    {
        // Debug.Log("In jump method:");
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        characterController.Move(playerVelocity * Time.deltaTime);
        // Reset jumping boolean
        allowedToJump = false;
    }

    private void ApplyGravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void ResetJump()
    {
        allowedToJump = true;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool getAllowedToJump()
    {
        return allowedToJump;
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }
}
