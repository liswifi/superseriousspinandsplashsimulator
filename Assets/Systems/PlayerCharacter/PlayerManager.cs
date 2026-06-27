using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameManager gameManager;
    InputManager inputManager;
    PlayerMovement playerMovement;
    public GameObject playerVis;
    public ParticleSystem splashParticles;
    bool hasDied = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        gameManager.spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessMovement();
        playerMovement.ProcessRotation();
        playerMovement.ProcessGravity();
        playerMovement.ProcessJumping();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            hasDied = true;
            // Go to gameManager and trigger transition UI, restart level
            Instantiate(splashParticles, transform.position, Quaternion.identity);
            gameManager.StartCoroutine("RestartGame", gameManager);
            Destroy(this.gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("WinVolume"))
        {
            gameManager.YouWin();
        }
    }
}
