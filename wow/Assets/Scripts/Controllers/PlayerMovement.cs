using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{


    private CharacterController characterController;
    public CharacterController controller;
    public Transform cam;

    //Motion
    private float speed = 50f;
    private float jumpHeight = 30f;
    private float gravity = -98.1f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 velocity;


    //animation
    public Animator anim;

    //Dash ability access
    public Vector3 moveDir;

    private void Start()
    {
        
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //playerGrounded = characterController.isGrounded;



        if (direction.magnitude >= 0.1f)
        {
            velocity.y += gravity * Time.deltaTime;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // x & z-axis movement, camera motion for 3-rd person
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);


        }

        // y-axis movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        anim.SetBool("isRunning", Input.GetAxis("Vertical") != 0);
        //anim.SetBool("Horizontal", Input.GetAxis("Horizontal"));
    }
}