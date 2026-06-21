using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 moveDir;
    InputManager inputManager;
    Rigidbody rb;
    public float moveSpeed = 5;
    float rotSpeed = 25;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void ProcessMovement()
    {
        moveDir = Camera.main.transform.forward * inputManager.ProcessMovementInput().y;
        moveDir = moveDir + Camera.main.transform.right * inputManager.ProcessMovementInput().x;
        moveDir.Normalize();
        moveDir.y = 0;

        moveDir = moveDir * moveSpeed;

        Vector3 moveVel = moveDir;
        rb.velocity = moveVel;
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
}
