using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private PlayerActions playerActions;
    private DialogueManager dialogueManager;
    private bool gameHasStarted = false;

    private void Awake()
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

    public void OnConfirm()
    {
        if (!gameHasStarted)
        {
            gameHasStarted = true;
            StartGame();
        }
        else
        {
            dialogueManager.DialogueToTextbox();
        }
    }

    public void OnExit()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        Debug.Log("Started game!");
    }
}
