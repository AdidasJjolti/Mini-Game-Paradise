using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BBGameManager : MonoBehaviour
{
    [SerializeField] UIManager _UIManager;
    bool _isGameOver;

    void Start()
    {
        _isGameOver = false;
    }

 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            if(_isGameOver == false)
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

    public void SetGameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f;
        BreakBreakScoreManager BBScoreManager = FindObjectOfType<BreakBreakScoreManager>();
        if (BBScoreManager != null)
        {
            BBScoreManager.SendMessage("SaveScore");             // 게임 오버일 때 점수 저장 하도록 메시지 전달
        }
        Debug.Log("게임 오버");
        _UIManager.SendMessage("OpenGameOverUI");
    }
}
