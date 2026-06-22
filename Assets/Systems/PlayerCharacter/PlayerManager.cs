using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMove()
    {
        if (inputManager != null && playerMovement != null)
        {
            playerMovement.ProcessMovement();
            playerMovement.ProcessRotation();
        }
    }

    public void OnJump()
    {
        if (!inputManager.jumpInput)
        {
            playerMovement.isJumping = true;
        }
        else
        {
            playerMovement.isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessGravity();
    }

}
