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
    public bool canInput = false;
    public Animator animator;

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
        if (canInput)
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

                    }
                    else
                    {
                        animator.SetBool("dialogueStarted", false);
                    }
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

    public void EnableInputs()
    {
        canInput = true;
    }

    private void StartGame()
    {
        if(animator != null)
        {
            introHasStarted = true;
            animator.SetBool("dialogueStarted", introHasStarted);
            dialogueManager.DialogueToTextbox();
        }
    }

    public void EnterGameplay()
    {
        SceneManager.LoadScene(1);
    }
}
