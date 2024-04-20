using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the player GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle player input in the Update method for responsiveness
        HandleInput();
    }

    void HandleInput()
    {
        // Get input axes for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        // If you want the player to jump, you can add code here to handle jump input

        // Apply the movement direction to the Rigidbody
        MovePlayer(movement);
    }

    void MovePlayer(Vector3 movement)
    {
        // Calculate the desired movement vector
        Vector3 moveVelocity = movement * moveSpeed;

        // Apply the movement to the Rigidbody
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }
}

