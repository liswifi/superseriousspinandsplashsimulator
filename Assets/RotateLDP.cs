using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLDP : MonoBehaviour
{
    public Vector3 rotateSpeeds;
    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(rb != null)
        {
            transform.Rotate(rotateSpeeds);
            rb.rotation = transform.rotation;
        }
    }
}
