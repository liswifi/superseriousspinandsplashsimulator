using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;
    public ParticleSystem splashParticles;

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

    private void OnTriggerEnter(Collider other)
    {
        // Go to gameManager and trigger transition UI, restart level
        Instantiate(splashParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
