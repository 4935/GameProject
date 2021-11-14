using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HoverCar : MonoBehaviour
{
    //Standard variables for speed
    public float fwd_accel = 100f;
    public float fwd_max_speed = 200f;
    public float brake_speed = 200f;
    public float turn_speed = 50f;

    public float hover_height = 3f;     //Distance to keep from the ground
    public float height_smooth = 10f;   //How fast the ship will readjust to "hover_height"
    public float pitch_smooth = 5f;     //How fast the ship will adjust its rotation to match track normal
    
    private Vector3 prev_up;
    public float yaw;
    private float smooth_y;
    private float current_speed;
    RaycastHit hit;

    float moveForce = 1.0f;
    float rotateTorque = 1.0f;
    float hoverHeight = 4.0f;
    float hoverForce = 5.0f;
    float hoverDamp = 1f;

    // Rigidbody component.
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Fairly high drag makes the object easier to control.
        rb.drag = 0.5f;
        rb.angularDrag = 0.5f;
    }

    void Update()
    {
        Vector3 interpolatedNormal = BarycentricCoordinateInterpolator.GetInterpolatedNormal(hit);

        rb.MoveRotation(Quaternion.FromToRotation(transform.up, interpolatedNormal) * rb.rotation);
    }

    void FixedUpdate()
    {

        // Push/turn the object based on arrow key input.
        rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward);


        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);

        // Cast a ray straight downwards.
        if (Physics.Raycast(downRay, out hit))
        {
            // The "error" in height is the difference between the desired height
            // and the height measured by the raycast distance.
            float hoverError = hoverHeight - hit.distance;

            // Only apply a lifting force if the object is too low (ie, let
            // gravity pull it downward if it is too high).
            if (hoverError > 0)
            {
                // Subtract the damping from the lifting force and apply it to
                // the rigidbody.
                float upwardSpeed = rb.velocity.y;
                float lift = hoverError * hoverForce - upwardSpeed * hoverDamp;
                rb.AddForce(lift * Vector3.up);
            }
        }
    }
}