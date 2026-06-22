using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component Refs")]
    InputManager inputManager;
    Rigidbody rb;

    [Header("Movement Mechanics")]
    public Vector3 moveDir;
    public float moveSpeed = 5;
    float rotSpeed = 25;

    [Header("Jumping Mechanics")]
    public bool isGrounded = false;
    public bool isJumping = false;
    public LayerMask groundLayer;
    public float airTime;
    public float jumpVelocity;
    public float dropVelocity;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void ProcessMovement()
    {
        if(inputManager != null)
        {
            moveDir = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * inputManager.ProcessMovementInput().y;
            moveDir = moveDir + Camera.main.transform.right * inputManager.ProcessMovementInput().x;
            moveDir.Normalize();
            moveDir = moveDir * moveSpeed;
            
            moveDir.y = rb.velocity.y; 

            Vector3 moveVel = moveDir;
            rb.velocity = moveVel;
        }
    }

    public void ProcessRotation()
    {
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
            rb.AddForce(transform.forward * jumpVelocity);
            rb.AddForce(-Vector3.up * dropVelocity * airTime);
        }

        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 5.0F, groundLayer))
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
        if (inputManager.jumpInput && isGrounded && (airTime <= 0.2))
        {
            rb.AddForce(Vector3.up * dropVelocity * airTime);

        }
    }
}
