using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateLDP : MonoBehaviour
{
    private Quaternion cachedRotation;
    public Vector3 rotateSpeeds;
    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        cachedRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if(rb != null)
        {
            Transform rotatorTransform = transform;
            rotatorTransform.Rotate(rotateSpeeds);

            rb.angularVelocity = new Vector3(rotatorTransform.rotation.x, rotatorTransform.rotation.y, rotatorTransform.rotation.z);

        }
    }
}
