using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private PlayerActions playerActions;
    [SerializeField] private DialogueManager dialogueManager;
    private bool introHasStarted = false;

    private void Start()
    {
        if (playerActions == null)
        {
            playerActions = new PlayerActions();
        }
        playerActions.Enable();
    }

    private void OnDestroy()
    {
        playerActions.Disable();
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!introHasStarted)
            {
                StartGame();
            }
            else
            {
                if (!dialogueManager.DialogueToTextbox())
                {
                    dialogueManager.DialogueToTextbox();
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    public void OnExit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Application.Quit();
        }
    }

    private void StartGame()
    {
        introHasStarted = true;
        Debug.Log("Started game!");
        dialogueManager.DialogueToTextbox();
    }
}
