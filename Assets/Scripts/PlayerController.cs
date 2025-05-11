using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // set variable
    // references
    [Header("References")]
    public CharacterController controller;
    public Transform cam;

    // basic player variables
    [Header("Basic Player Movement Info")]
    public float crouchSpeed = 2.5f;
    public float walkSpeed = 5;
    public float sprintSpeed = 10;
    public float jumpHeight = 2;
    public float gravity = -9.8f;
    public float acceleration = 0.5f;

    // make float for moveSpeed
    float moveSpeed;

    // angle smoothing
    [Header("Player rotation smoothing")]
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // jump/grounding variables
    [Header("Jump/Grounding Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    // Crouch Variables
    [Header("Crouch")]
    bool isCrouching = false;
    public float crouchHeight = 1;
    public float standHeight = 2;
    float currentHeight;

    // cinemachine camera
    public CinemachineOrbitalFollow cameraRig;
    public CinemachineRotationComposer camRotation;
    private void Start()
    {
        // set a speed of movement to the walking speed
        moveSpeed = walkSpeed;
        // set current height of collider and regular height of walking/running
        currentHeight = controller.height;
        standHeight = currentHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // check if the players is grounded by forming a sphere at groundCheck with the radius of groundDistance
        // then see if this sphere has collided with an object with a layer mask equal to groundMask
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // ensures the player is firmly on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // walk, run and crouch
        if (isGrounded && Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        // get input values
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical"); // also known as y in 2d graph

        // set a nomalized vector for movement based on input
        Vector3 direction = new Vector3(x, 0, z).normalized;

        if (isCrouching == true)
        {
            // when crouching
            // lerp between the current speed toward the crouching speed by gradually decreasing by the acceleration over time
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
            // change character controller collider height smoothly to crouch height
            currentHeight = Mathf.Lerp(currentHeight, crouchHeight, acceleration * Time.deltaTime);
        }
        else 
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            // change character controller collider height smoothly to walk height
            currentHeight = Mathf.Lerp(currentHeight, standHeight, acceleration * Time.deltaTime);
        }
        // apply height changes
        controller.height = currentHeight;
        // apply collision position changes
        controller.center = new Vector3(0, currentHeight / 2, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // when holding the sprint key
            // lerp between the current speed of movement to the running speed by increasing gradually by the acceleration over time
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // when sprint key is released
            // lerp between the current speed of movement to the walking speed by increasing/decreasing
            // gradually by the acceleration over time
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }

        // check their has been input
        if (direction.magnitude >= 0.1f)
        {
            // find the target angle based on movement then smooth and apply
            // take into account the camera angles
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // make new movement vector 3 based of the direction the camera is facing on y axis
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            // apply movement
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // calculates and adds force for jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // applies gravity
        velocity.y += gravity * Time.deltaTime;

        // applies to player movement the velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
