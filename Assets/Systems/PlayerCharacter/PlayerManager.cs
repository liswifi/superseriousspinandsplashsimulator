using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMove()
    {
        playerMovement.ProcessMovement();
        playerMovement.ProcessRotation();
    }

}
