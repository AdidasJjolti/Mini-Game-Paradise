using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGate : MonoBehaviour
{
    public float SetPosX(int n)
    {
        float x;

        if(n % 2 == 0)
        {
            x = Random.Range(-2f, -1f);
        }
        else
        {
            x = Random.Range(1.0f, 2.0f);
        }

        return x;
    }
}
