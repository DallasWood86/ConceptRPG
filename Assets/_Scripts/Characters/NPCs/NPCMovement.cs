using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NPCMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private float moveSpeed = 8f;
    private float rotationSpeed = 5f;

    public Transform groundCheck;
    public LayerMask groundMask;
    private float groundDistance = 0.4f;
    private float gravity = -9.8f * 5f;

    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ApplyGravity();
    }

    public void MoveTowards(Transform target)
    {
        //  rotate towards where we are moving to
        RotateTowards(target.position);

        var offset = target.position - transform.position;
        offset.y = 0;
        //Get the difference.
        if (offset.magnitude > .1f)
        {

            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            offset = offset.normalized * moveSpeed;
            //normalize it and account for movement speed.



            controller.Move(offset * Time.deltaTime);
            //actually move the character.
        }
    }

    public void MoveTowards(Vector3 target)
    {
        //  rotate towards where we are moving to
        RotateTowards(target);
        //Get the difference
        var offset = target - transform.position;
        offset.y = 0;

        float curSpeed = offset.normalized.magnitude;
        animator.SetFloat("Velocity", offset.magnitude);
        // Debug.Log();

        if (offset.magnitude > .1f)
        {
            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            offset = offset.normalized * moveSpeed;

            //normalize it and account for movement speed.
            controller.Move(offset * Time.deltaTime);
            //actually move the character.
        }
    }

    public void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction.sqrMagnitude > Mathf.Epsilon)
        {
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    private void ApplyGravity()
    {
        // reset our y velocity if already grounded
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //  apply our gravity movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

}
