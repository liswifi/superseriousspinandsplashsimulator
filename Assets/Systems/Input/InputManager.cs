using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerActions playerActions;
    private InputSpin inputSpin;
    public Vector2 movementInput;
    public bool jumpInput;

    private void Awake()
    {
        if (playerActions == null)
        {
            playerActions = new PlayerActions();
            playerActions.Gameplay.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerActions.Gameplay.Jump.performed += i => jumpInput = i.ReadValueAsButton();
        }

        playerActions.Enable();

        inputSpin = GetComponent<InputSpin>();
    }

    private void OnDestroy()
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
}
