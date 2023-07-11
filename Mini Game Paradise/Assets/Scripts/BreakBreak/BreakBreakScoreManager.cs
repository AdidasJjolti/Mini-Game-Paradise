using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface IObserver
{
    public void ScoreUpdate(string subject);
}

public class BreakBreakScoreManager : MonoBehaviour, IObserver
{
    int _curScore;
    int _highestScore;
    bool _isScoreRenewed;
    [SerializeField] BlockBreaker _blockBreaker;
    [SerializeField] TextMeshProUGUI _curScoreText;
    [SerializeField] TextMeshProUGUI _highestScoreText;

    void Start()
    {
        _blockBreaker.RegisterObserver(this.GetComponent<IObserver>());
        _curScore = 0;
        List<int> records = BB_Records.LoadScoresFromCSV();
        if(records.Count > 0)
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
        switch(subject)
        {
            case "BlockBreaker":
                _curScore += 100;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case "Stun":
                _curScore += 150;
                //Debug.Log("Current Score is " + _curScore);
                break;
        }

        if(_curScore > _highestScore)
        {
            _isScoreRenewed = true;
        }

        SetScoreText();
    }

    // ���������� ���� ȹ���ϴ� ��� ���� ������ �߰�
    public void ItemScoreUpdate(_eItemType type)
    {
        switch(type)
        {
            case _eItemType.YELLOW_SINGLE:
                _curScore += 10;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eItemType.ORANGE_SINGLE:
                _curScore += 10;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eItemType.GREEN_SINGLE:
                _curScore += 10;
                //Debug.Log("Current Score is " + _curScore);
                break;

            case _eItemType.YELLOW_DOUBLE:
                _curScore += 20;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eItemType.ORANGE_DOUBLE:
                _curScore += 20;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eItemType.GREEN_DOUBLE:
                _curScore += 20;
               // Debug.Log("Current Score is " + _curScore);
                break;

            case _eItemType.YELLOW_TRIPLE:
                _curScore += 30;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eItemType.ORANGE_TRIPLE:
                _curScore += 30;
                //Debug.Log("Current Score is " + _curScore);
                break;
            case _eItemType.GREEN_TRIPLE:
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

    // ȭ�� ���ܿ� ���� ����, �ְ� ���� �ؽ�Ʈ ����
    public void SetScoreText()
    {
        _curScoreText.text = _curScore.ToString();

        if(_isScoreRenewed)
        {
            _highestScoreText.text = _curScore.ToString();
        }
    }

    // �ְ� ��� ���ŵ� �� PlayerPrefs�� �ְ� ��� ����, CSV ���Ϸ� ��ü ����� �Ѱ��ִ� �ڵ�� �����ϸ鼭 �ʿ� ������
    //public void SaveScore()
    //{
    //    if(_isScoreRenewed)
    //    {
    //        PlayerPrefs.SetInt("BB_bestScore", _curScore);
    //    }
    //}

    // ���� ���� UI�� ���� ��� ���޿� �Լ�
    public int GetCurScore()
    {
        return _curScore;
    }
}
