using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerRef;
    public Vector3 playerOffset;
    [Range(0.001F, 0.1F)] public float cameraDampening;

    void Awake()
    {
        playerRef = FindFirstObjectByType<PlayerManager>().gameObject;
    }

    void FixedUpdate()
    {
        if (playerRef != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerRef.transform.position + playerOffset, Time.deltaTime + cameraDampening);
        }
    }
}
