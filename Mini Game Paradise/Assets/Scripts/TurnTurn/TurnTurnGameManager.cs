using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnTurnGameManager : MonoBehaviour
{
    [SerializeField] TurnTurnUIManager _UIManager;
    bool _isGameOver;
    [SerializeField] int _count;

    void Start()
    {
        _isGameOver = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (_isGameOver == false)
            {
                SetGameOver();
            }
            else
            {
                _isGameOver = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void CountGates()
    {
        _count++;
        Debug.Log($"��� ����Ʈ : {_count}��");
    }

    public int GetGateCount()
    {
        return _count;
    }

    public void SetGameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("���� ����");
        _UIManager.SendMessage("OpenGameOverUI");
    }
}
