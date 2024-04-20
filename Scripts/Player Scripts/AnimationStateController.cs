using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;

    // Player Input variables - will only use vertical for now, horizontal animations if time.
    public float horizontalInput;
    public float verticalInput;
    public bool clickBool;
    public bool sprintInput;
    public bool jumpInput;
    public bool interactionInput;
    public int playerTool;

    private bool allowedToJump;
    private bool isGrounded;
    private bool allowedToInteract;


    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // get player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        jumpInput = Input.GetKey(KeyCode.Space);
        interactionInput = Input.GetKey(KeyCode.Space);

        CalculateAnimation();
    }

    private void CalculateAnimation()
    {
        // If idle, there is no user input
        if (verticalInput == 0f && horizontalInput == 0f && !jumpInput && !interactionInput)
        {
            ResetAnimation();
            IdleAnimation();
        }
        // if Walking forward
        else if (verticalInput != 0f && sprintInput)
        {
            ResetAnimation();
            RunAnimation();
        }

        //Run animation
        else if (verticalInput != 0f || horizontalInput != 0f && !jumpInput && !interactionInput)
        {
            ResetAnimation();
            WalkAnimation();
        }

        // Jump animation
        else if (jumpInput)
        {
            allowedToJump = playerMovement.getAllowedToJump();
            isGrounded = playerMovement.getIsGrounded();

            // Jump input
            if (jumpInput && allowedToJump && isGrounded)
            {
                ResetAnimation();
                JumpAnimation();
            }
        }
    }


    public void WalkAnimation()
    {
        // Set the correct animation state to true
        animator.SetBool("isWalking", true);
    }


    public void IdleAnimation()
    {
        // Set the correct animation state to true
        animator.SetBool("isIdle", true);
    }

    public void RunAnimation()
    {
        // Debug.Log("Run Animation");
        // Set the correct animation state to true
        animator.SetBool("isRunning", true);
    }

    public void ClickAnimation()
    {
        // Set the correct animation state to true
        animator.SetBool("isShooting", true);
    }

    public void JumpAnimation()
    {
        // Set the correct animation state to true
        animator.SetBool("isJumping", true);
    }

    private void ResetAnimation()
    {
        // Reset animation state bools
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", false);
    }

    private void GetPlayerTool()
    {

    }
}
