using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    public int spinsDone = 0;

    void Update()
    {
        
    }

    public void CheckSpin(float spinVelocity)
    {
        if(spinVelocity > Mathf.Epsilon || spinVelocity < Mathf.Epsilon)
        {
            ++spinsDone;
        }
    }
}
