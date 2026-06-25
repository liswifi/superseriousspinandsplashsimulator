using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component Refs")]
    InputManager inputManager;
    Rigidbody rb;
    Animator animator;

    [Header("Movement Mechanics")]
    public bool isGrounded = false;
    public Vector3 moveDir;
    public float moveSpeed = 5;
    float rotSpeed = 25;
    public LayerMask groundLayer;

    [Header("Jumping Mechanics")]
    public bool isJumping = false;
    public float airTime;
    public float jumpVelocity;
    public float dropVelocity;
    public float jumpHeight = 5;
    public float gravity = -9;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    public void ProcessMovement()
    {
        if(inputManager != null)
        {
            moveDir = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * inputManager.ProcessMovementInput().y;
            moveDir = moveDir + Camera.main.transform.right * inputManager.ProcessMovementInput().x;
            moveDir.Normalize();
            moveDir.y = 0;

            Vector3 moveVel = moveDir * moveSpeed;
            moveVel.y = rb.velocity.y;
            rb.velocity = moveVel;

            bool moveAnim = moveDir != Vector3.zero ? true : false;
            animator.SetBool("isMoving", moveAnim);
        }
    }

    public void ProcessRotation()
    {
        if (isJumping)
        {
            return;
        }

        Vector3 targetDir = Vector3.zero;

        targetDir = Camera.main.transform.forward * inputManager.ProcessMovementInput().x;
        targetDir = targetDir + Camera.main.transform.right * -inputManager.ProcessMovementInput().y;
        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        Quaternion playerRot = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);

        transform.rotation = playerRot;
    }

    public void ProcessGravity()
    {
        RaycastHit hit;

        if (!isGrounded)
        {
            airTime = airTime + Time.deltaTime;
            if (inputManager.jumpInput)
            {
                rb.AddForce(transform.forward * jumpVelocity);
                rb.AddForce(-Vector3.up * dropVelocity * airTime);
                if(airTime > 1.0F)
                {
                    airTime = 0;
                    isJumping = false;
                    inputManager.jumpInput = false;
                }
            }
        }

        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2F, groundLayer))
        {
            if (!isGrounded)
            {
                // Land anim
            }

            airTime = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void ProcessJumping()
    {
        if (isGrounded && inputManager.jumpInput)
        {
            isJumping = true;
            float jumpVel = Mathf.Sqrt(-2 * gravity * jumpHeight);
            Vector3 playerVel = moveDir;
            playerVel.y = jumpVel;
            rb.velocity = playerVel;
            isJumping = false;
        }
        if (!isGrounded && !inputManager.jumpInput)
        {
            rb.AddForce(transform.forward * jumpVelocity);
            rb.AddForce(-Vector3.up * dropVelocity * airTime * 50);
            isJumping = false;
        }
        bool jumpAnim = rb.velocity.y > 1.0F ? true : false;
        animator.SetBool("isJumping", jumpAnim);
    }
}
