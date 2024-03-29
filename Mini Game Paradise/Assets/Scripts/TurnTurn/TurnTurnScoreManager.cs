using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TurnTurnScoreManager : MonoBehaviour
{
    int _curScore;
    int _highestScore;
    bool _isScoreRenewed;

    [SerializeField] TextMeshProUGUI _curScoreText;
    [SerializeField] TextMeshProUGUI _highestScoreText;

    void Start()
    {
        _curScore = 0;
        List<int> records = TurnTurn_Records.LoadScoresFromCSV();
        if (records.Count > 0)
        {
            _highestScore = records[0];
        }
        else
        {
            _highestScore = 0;
        }
        _curScoreText.text = _curScore.ToString();
        _highestScoreText.text = _highestScore.ToString();

        _isScoreRenewed = false;
    }

    public void ScoreUpdate(string subject)
    {
        switch (subject)
        {
            case "Tree":
                _curScore += 100;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case "Pass":
                _curScore += 150;
                //Debug.Log("Current Score is " + _curScore);
                break;
        }

        if (_curScore > _highestScore)
        {
            _isScoreRenewed = true;
        }

        SetScoreText();
    }

    // 아이템으로 점수 획득하는 경우 현재 점수에 추가
    public void ItemScoreUpdate(_eTurnTurnItemType type)
    {
        switch (type)
        {
            case _eTurnTurnItemType.YELLOW_SINGLE:
                _curScore += 10;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eTurnTurnItemType.ORANGE_SINGLE:
                _curScore += 10;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eTurnTurnItemType.GREEN_SINGLE:
                _curScore += 10;
                //Debug.Log("Current Score is " + _curScore);
                break;

            case _eTurnTurnItemType.YELLOW_DOUBLE:
                _curScore += 20;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eTurnTurnItemType.ORANGE_DOUBLE:
                _curScore += 20;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eTurnTurnItemType.GREEN_DOUBLE:
                _curScore += 20;
                // Debug.Log("Current Score is " + _curScore);
                break;

            case _eTurnTurnItemType.YELLOW_TRIPLE:
                _curScore += 30;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eTurnTurnItemType.ORANGE_TRIPLE:
                _curScore += 30;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eTurnTurnItemType.GREEN_TRIPLE:
                _curScore += 30;
                //Debug.Log("Current Score is " + _curScore);
                break;
        }

        if (_curScore > _highestScore)
        {
            _isScoreRenewed = true;
        }

        SetScoreText();
    }

    // 화면 우상단에 현재 점수, 최고 점수 텍스트 갱신
    public void SetScoreText()
    {
        _curScoreText.text = _curScore.ToString();

        if (_isScoreRenewed)
        {
            _highestScoreText.text = _curScore.ToString();
        }
    }

    // 최고 기록 갱신될 때 PlayerPrefs에 최고 기록 저장, CSV 파일로 전체 기록을 넘겨주는 코드로 수정하면서 필요 없어짐
    //public void SaveScore()
    //{
    //    if(_isScoreRenewed)
    //    {
    //        PlayerPrefs.SetInt("BB_bestScore", _curScore);
    //    }
    //}

    // 게임 오버 UI에 현재 기록 전달용 함수
    public int GetCurScore()
    {
        return _curScore;
    }
}
