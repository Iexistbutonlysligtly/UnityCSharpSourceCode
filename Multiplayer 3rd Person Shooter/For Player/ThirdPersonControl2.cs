using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonControl2 : MonoBehaviour
{
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;


    [Range(0, 1)]
    public float airControlPercent;

    float velocityY; //Stores the velocity on the Y Axsis
    public float jumpHeight = 10;


    Animator animator;


    Transform TargetCamera;

    CharacterController ObjectController;
    
    void Start()
    {
        animator = GetComponent<Animator> ();
        ObjectController = GetComponent<CharacterController>();
        TargetCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.X))
        {

            Jump();

        }


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Makes Vector for the Horizontal and Vertical Inputs for Input Keys
        Vector2 inputDir = input.normalized; //Turns the input Vector into a Direction

        if (inputDir != Vector2.zero) //The Rotation is only Calculated if the Input Direction is 0,0
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + TargetCamera.eulerAngles.y; //Calculates the Rotation
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, ModifiedSmoothTime(turnSmoothTime)); // Rotates object until it reaches the rotation set above by the speed set
        }

        bool running = Input.GetKey(KeyCode.JoystickButton6) || Input.GetKey(KeyCode.LeftShift); //What Buttons to hold to Run
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;  // Logic of lines says if (running) is true(?) then the objects speed is set to run speed. If (running isnt true(:) then it equals walk speed. Then multiply by magnitute as it ill be 0 if walk or run isnt true and thus make speed 0.
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, ModifiedSmoothTime(speedSmoothTime));  //Objects speed value will go up or down till it reaches the target speed 



        velocityY += Time.deltaTime * gravity; // Keeps the player going down on the Y Axsis, simulating Gravity

        Vector3 ObjectVelocity = transform.forward * targetSpeed + Vector3.up * velocityY ; //Initiates the direction the Object moves to where its facing, and gives the inital speed. Then moves the player on the Y axsis with the VelocityY amount

        ObjectController.Move(ObjectVelocity * Time.deltaTime); //Makes the CharacterController attached to Object move in the Direction and Speed set by the Vector, times by DeltaTime


        currentSpeed = new Vector2(ObjectController.velocity.x, ObjectController.velocity.z).magnitude;

       if (ObjectController.isGrounded)
        {
            velocityY = 0;

        }



        float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f); // If (running) is true(?), then divide the run speed by the run speed so the threshold is 1 which plays the run animation. If ts not true (:) then divide the walk speed by walk speed divide by 0.5 to half it to use 0.5 as the threshold for the walk animation
        animator.SetFloat("SpeedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime); //Sets the value of the Float Peramiter for the Animation to qual the float that changes to the walking and running values
    }

    void Jump()
    {

        if (ObjectController.isGrounded)
        {

            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight); // Kimimatic Equation for the Jumping
            velocityY = jumpVelocity;
        }


        


    }

    float ModifiedSmoothTime(float smoothTime)
    {

        if (ObjectController.isGrounded)
        {
            return smoothTime;
        }


        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }


        return smoothTime / airControlPercent;

    }
}
