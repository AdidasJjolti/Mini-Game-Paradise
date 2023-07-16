using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGate : MonoBehaviour
{
    [SerializeField] TurnTurnGameManager _gameManager;

    void Awake()
    {
        if(_gameManager == null)
        {
            _gameManager = FindObjectOfType<TurnTurnGameManager>();
        }
    }

    public float SetPosX(int n)
    {
        float x;

        if(n % 2 == 0)
        {
            x = Random.Range(GateData.SetHorizontalDistance(_gameManager.GetGateCount()).Item2 * -1, -GateData.SetHorizontalDistance(_gameManager.GetGateCount()).Item1 * -1);
        }
        else
        {
            x = Random.Range(GateData.SetHorizontalDistance(_gameManager.GetGateCount()).Item1, GateData.SetHorizontalDistance(_gameManager.GetGateCount()).Item2);
        }

        return x;
    }
}
