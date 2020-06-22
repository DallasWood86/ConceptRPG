using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundMask;
    private float groundDistance = 0.4f;

    private CharacterController controller;
    private Animator animator;

    Vector2 movementInput = Vector2.zero;
    private Vector3 velocity;
    private float gravity = -9.8f * 5f;
    private float moveSpeed = 12f;
    private float moveSpeedBackwards = -1f;
    private float moveSpeedStrafe = 1f;
    private float jumpHeight = 4f;
    private float currentSpeed;
    private float currentStrafeSpeed;
    private float speedSmoothVelocity;
    private float speedSmoothTime = 0.1f;
    private float rotationSpeed = 250f;

    private bool airborn;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //  if we jumped, check if we landed to play land animation
        if (airborn && IsGrounded())
        {
            animator.SetTrigger("Land");
            airborn = false;
        }
    }

    public void Move()
    {
        // reset our y velocity if already grounded
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //  get our inputs if we are on the ground
        // if (IsGrounded())
        {
            movementInput = new Vector2(GetStrafeInput(), Input.GetAxis("Vertical"));
        }

        //  calculate target speed
        float targetSpeed = moveSpeed * movementInput.normalized.magnitude;
        float targetSpeedStrafe = moveSpeedStrafe * movementInput.normalized.magnitude;

        //  calculate the speeds
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        currentStrafeSpeed = Mathf.SmoothDamp(currentStrafeSpeed, targetSpeedStrafe, ref speedSmoothVelocity, speedSmoothTime);

        //  clamp the speed between the max backwards speed and max speed
        currentSpeed = Mathf.Clamp(currentSpeed, moveSpeedBackwards, moveSpeed);

        // apply our speed to the animator
        animator.SetFloat("MoveSpeed", movementInput.y, speedSmoothTime, Time.deltaTime);
        animator.SetFloat("StrafeSpeed", movementInput.x, speedSmoothTime, Time.deltaTime);

        //  calculate the direction we want to move in. normalize it so diagonal direction doesnt add speed
        Vector3 moveDirection = ((transform.right * movementInput.x) + (transform.forward * movementInput.y)).normalized;

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        //  apply our gravity movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //  apply rotation if needed
        // Rotate();
    }

    public void StartJump()
    {
        animator.SetTrigger("Jump");
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void Rotate()
    {
        float rotateInput;

        if (Input.GetButton("Fire2"))
        {
            rotateInput = Input.GetAxis("Mouse X");
        }
        else
        {
            rotateInput = Input.GetAxis("Horizontal");

        }
        transform.eulerAngles += new Vector3(0, rotateInput * rotationSpeed * Time.deltaTime, 0);

    }

    private float GetStrafeInput()
    {
        float strafeInput;

        if (Input.GetButton("Fire2"))
        {
            strafeInput = Input.GetAxis("Horizontal");
        }
        else
        {
            strafeInput = Input.GetAxis("Strafe");
        }

        return strafeInput;
    }

    public void SetAirborn()
    {
        airborn = true;
    }
}
