using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform mainCamera;
    //public Transform playerLeftArm;
    //public Transform playerRightArm;
    public float sensitivity;
    public bool isPaused = false;
    public Transform playerBody;
    public float mouseX;
    public float mouseY;

    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;
    //Vector2 rotation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse input
        mouseX = Input.GetAxisRaw("Mouse X") * Time.smoothDeltaTime * sensitivity;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.smoothDeltaTime * sensitivity;

        //mouseX = Input.GetAxisRaw("Mouse X") *  sensitivity;
        //mouseY = Input.GetAxisRaw("Mouse Y") *  sensitivity;

        // Update the roation values of the player based on mouse input
        // X movement rotates the player around the y axis, meaning mouse x should adjust roationY
        yRotation = yRotation + mouseX;
        xRotation = xRotation - mouseY;


        //clamp rotation about the x axis
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate these objects using x and y rotation
        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);


        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void LateUpdate()
    {
 
    }
}
