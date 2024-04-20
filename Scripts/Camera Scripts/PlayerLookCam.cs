using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookCam : MonoBehaviour
{
    //declare variables
    public float sensitivity;
    public Transform playerDirection;
    public Transform playerLeftArm;
    public Transform playerRightArm;

    //Camera variables 
    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the players mouse when they are playing the game
        // Might move this code to a game controller - so easier to know when it gets changed
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse input
        float inputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float inputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        // Update the roation values of the player based on mouse input
        // X movement rotates the player around the y axis, meaning mouse x should adjust roationY
        yRotation = yRotation + inputX;
        xRotation = xRotation - inputY;


        //clamp rotation about the x axis
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ======== Free movement ========
        // Camera, Players Weapon, Left Arm, Right Arm.
        
        // rotate these objects using x and y rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //playerLeftArm.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //playerRightArm.rotation = Quaternion.Euler(xRotation, yRotation, 0);



        // ======== Restricted Movement ========
        // APP2 Featured Superman when you looked at the ground or sky.
        // This is better than my APP2 as APP2, allow the player to rotate all over the place.
        playerDirection.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
