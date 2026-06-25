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
    ParticleSystem particleSystem;

    [Header("Movement")]
    public bool isMoving = false;
    public bool onGround = false;
    private Vector3 moveDir;
    private Vector3 targetDir;
    public float moveSpeed = 5;
    float rotSpeed = 25;
    public LayerMask groundLayer;

    [Header("Physics")]
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
        particleSystem = GetComponentInChildren<ParticleSystem>();
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

            isMoving = moveDir != Vector3.zero;
            animator.SetBool("isMoving", isMoving);

            if (isMoving && onGround) {
                particleSystem.Play();
            }
            else
            {
                particleSystem.Stop();
            }
        }
    }

    public void ProcessRotation()
    {
        if (isJumping)
        {
            return;
        }

        targetDir = Vector3.zero;

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

        float angularSpeed = Quaternion.Angle(playerRot, targetRot) / Time.deltaTime;
        rb.angularVelocity = new Vector3(0.0F, angularSpeed, 0.0F);
        bool isSpinning = rb.angularVelocity.z > 5.0F || rb.angularVelocity.z < 5.0F;
    }

    public void ProcessGravity()
    {
        RaycastHit hit;

        if (!onGround)
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
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2F, groundLayer))
        {
            airTime = 0;
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    public void ProcessJumping()
    {
        if (onGround && inputManager.jumpInput)
        {
            isJumping = true;
            float jumpVel = Mathf.Sqrt(-2 * gravity * jumpHeight);
            Vector3 playerVel = moveDir;
            playerVel.y = jumpVel;
            rb.velocity = playerVel;
            isJumping = false;
        }
        if (!onGround && !inputManager.jumpInput)
        {
            rb.AddForce(transform.forward * jumpVelocity);
            rb.AddForce(50 * airTime * dropVelocity * -Vector3.up);
            isJumping = false;
        }
        bool jumpAnim = rb.velocity.y > 1.0F || rb.velocity.y < -1.0F;
        animator.SetBool("isJumping", jumpAnim);
    }
}
