using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
   // private CharacterController controller;
    public float rotationSpeed = 50;
   // private Vector3 playerVelocity;
    public float playerSpeed = 2.0f;
    //private float gravityValue = -9.81f;
    public Joystick joystick;
   // InputAction movement;
    //private Vector3 rotation;
    public Animator anim;
    Rigidbody rb;
    public float z;
    public float x;

 

    private void Start()
    {
        // controller = gameObject.AddComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        // movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        //  movement.Enable();
    }

    void FixedUpdate()
    {

        z = joystick.Horizontal;
        x = joystick.Vertical * -1;

 

        Vector3 move = new Vector3(x, 0, z);

     
            rb.MovePosition(transform.position + move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                transform.forward = move;
                anim.SetBool("isRunning", true);
               
            }
            else
            {
                anim.SetBool("isRunning", false);
            }


        

    }

}