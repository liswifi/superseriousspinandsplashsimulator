using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Component Refs")]
    private PlayerActions playerActions;
    private PlayerMovement playerMovement;
    [Header("Input Values")]
    public Vector2 movementInput;
    public bool jumpInput;

    private void OnEnable()
    {
        if (playerActions == null)
        {
            playerActions = new PlayerActions();
            playerActions.Gameplay.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerActions.Gameplay.Jump.performed += i => jumpInput = true;
            playerActions.Gameplay.Jump.canceled += i => jumpInput = false;
        }

        playerActions.Enable();

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    public Vector2 ProcessMovementInput()
    {
        if(movementInput != null)
        {
            var xInput = movementInput.x;
            var yInput = movementInput.y;
            Vector2 processedInput = new Vector2(xInput, yInput);
            return processedInput;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public void ProcessJumpInput()
    {
        if (jumpInput && !playerMovement.isJumping)
        {
            playerMovement.isJumping = true;
        }
    }
}
