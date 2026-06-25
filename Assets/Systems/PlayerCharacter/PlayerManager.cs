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

    private void FixedUpdate()
    {
        playerMovement.ProcessMovement();
        playerMovement.ProcessRotation();
        playerMovement.ProcessGravity();
        playerMovement.ProcessJumping();
    }

}
