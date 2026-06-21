using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputSpin : MonoBehaviour
{
    InputManager inputManager;
    private float[] angleValues = new float[8] { 0.0F, 45.0F, 90.0F, 135.0F, 179.0F, -135.0F, -90.0F, -45.0F };
    private int currentQuad;
    public float angle;
    private Vector2 input;
    
    void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        ProcessInput();
    }

    float ProcessInput() // DEBUG
    {
        input = inputManager.movementInput;
        angle = Mathf.Atan2(input.x, input.y);
        angle = angle * Mathf.Rad2Deg;

        return angle;
    }

    public bool StartInput()
    {
        bool _hasStarted = false;

        if (angleValues.Contains(angle))
        {
            _hasStarted = true;
        }
        if (_hasStarted)
        {
            if((input.x + input.y) == 0)
            {
                _hasStarted = false;
            }
            CheckAngle();
            Debug.Log("is checking input");
        }

        return angleValues.Contains(angle);
    }

    void CheckAngle()
    {
        for (int i = 0; i < angleValues.Length; i++)
        {
            if (angle == angleValues[i])
            {
                currentQuad = i;
                Debug.Log("Current quad is: " + i);
            }
        }
    }
}
