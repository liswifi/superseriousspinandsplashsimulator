using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject playerRef;
    public Vector3 playerOffset;

    void Awake()
    {
        playerRef = FindFirstObjectByType<PlayerManager>().gameObject;
    }

    void FixedUpdate()
    {
        if (playerRef != null)
        {
            transform.position = playerRef.transform.position + playerOffset * Time.deltaTime;
        }
    }
}
