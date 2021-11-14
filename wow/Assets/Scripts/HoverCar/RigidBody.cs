using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour
{
    Rigidbody rb;

    //Ship movement variables
    [Header("Movement")]
    public float accel;
    public float maxSpeed;
    public float brakeSpeed;
    private float currentSpeed;

    //Ship force variables (Currently Unused)
    [Header("Forces")]
    public float moveForce;
    public float rotateTorque;
    public float hoverHeight;
    public float hoverForce;
    public float hoverDamp;



    //Auto adjust to track surface parameters
    [Header("Height and Rotation")]
    public float height;
    public float heightAdjust;
    public float rotationgAdjust;
    public float rotationTorque;

    //We will use all this stuff later
    [Header("Other")]
    private Vector3 prev_up;
    public float yaw;
    private float yAdjust;
    public Vector3 pullForce;


    void Start()
    {

    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            currentSpeed += (currentSpeed >= maxSpeed) ? 0f : accel * Time.deltaTime;
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= brakeSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed = 0f;
            }
        }

        transform.position += transform.forward * (currentSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {

        rb = GetComponent<Rigidbody>();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -prev_up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Vector3 interpolatedNormal = BCInterpolator.GetInterpolatedNormal(hit);

            rb.MoveRotation(Quaternion.FromToRotation(transform.up, interpolatedNormal) * rb.rotation);
        }

        rb.AddTorque(Input.GetAxis("Horizontal") * rotationTorque * Vector3.up);

        //yaw += turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        prev_up = transform.up;
        //rb.rotation = Quaternion.Euler(0, yaw, 0);

        if(hit.distance > hoverHeight + 0.5)
        {
            rb.AddRelativeForce(pullForce);
        }
        else if (hit.distance < hoverHeight - 0.5)
        {
            rb.AddRelativeForce(-pullForce);
        }
    }

}
