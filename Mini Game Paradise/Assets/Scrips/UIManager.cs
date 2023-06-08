using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _settingsUI;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] TextMeshProUGUI _curScore;

    [SerializeField] Slider _BGMSlider;
    [SerializeField] Slider _SFXSlider;

    ScoreItem _scoreItem;

    void Awake()
    {
        _scoreItem = FindObjectOfType<ScoreItem>(true);

        if(!_settingsUI)
        {
            _settingsUI = GameObject.Find("Canvas").transform.Find("SettingsUI").gameObject;
        }

        if (!_gameOverUI)
        {
            _gameOverUI = GameObject.Find("Canvas").transform.Find("GameOverUI").gameObject;
        }

        var sliders = FindObjectsOfType<Slider>(true);

        if(!_BGMSlider)
        {
            foreach (var slider in sliders)
            {
                if (slider.transform.name == "BGMSlider")
                {
                    _BGMSlider = slider;
                }
            }
        }

        if (!_SFXSlider)
        {
            foreach (var slider in sliders)
            {
                if (slider.transform.name == "SFXSlider")
                {
                    _SFXSlider = slider;
                }
            }
        }

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            _BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            _BGMSlider.value = 1f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            _SFXSlider.value = 1f;
        }

        _BGMSlider.onValueChanged.AddListener(BGMOnValueChanged);
        _SFXSlider.onValueChanged.AddListener(SFXOnValueChanged);

        var texts = FindObjectsOfType<TextMeshProUGUI>(true);

        if (!_curScore)
        {
            foreach (var text in texts)
            {
                if (text.transform.name == "CurrentRecord")
                {
                    _curScore = text;
                }
            }
        }
    }

    public void OnClickSettingsButton()
    {
        _settingsUI.SetActive(true);
        SoundManager.Instance.PlayButtonClickSound();
        Time.timeScale = 0;
    }

    public void OnClickResumeButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayButtonClickSound();
        _settingsUI.SetActive(false);
    }

    public void OnClickMainButton()
    {
        // ToDo : ���� ȭ�� ����� �����ϱ�
    }

    public void BGMOnValueChanged(float bgmVolume)
    {
        SoundManager.Instance.SetBGMVolume(bgmVolume);
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
    }

    public void SFXOnValueChanged(float sfxVolume)
    {
        SoundManager.Instance.SetSFXVolume(sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void OpenGameOverUI()
    {
        _gameOverUI.SetActive(true);

        // ToDo : ���� ���� 5�� ��� �����ϱ�
        List<int> records = BB_Records.LoadScoresFromCSV();
        List<int> beforeRanking = new List<int>();
        List<int> afterRanking = new List<int>();

        if(records.Count == 0)
        {
            SaveRecords();
            ShowRecords();
            //_newIcons[0].gameObject.SetActive(true);   // 1�� ��Ͽ� new ������ ǥ��
            _scoreItem.SendMessage("SwitchOnNewIcon", 0);
            afterRanking = SaveRankings();
        }
        else          // ������ ����� 1�� �̻��̸� ���� ��� �����ϱ� ���� ���� ���� 5�� ����� ���� ���� �� ��� ����, ��� ǥ�� ����
        {
            beforeRanking = SaveRankings();

            SaveRecords();
            ShowRecords();

            afterRanking = SaveRankings();

            // ���� ���� 5�� ��ϰ� ���� �� ���� 5�� ����� ���Ͽ� �ٲ� ����� ������ �ش� ��Ͽ� new ������ ǥ��
            for (int i = 0; i < Mathf.Clamp(afterRanking.Count, afterRanking.Count, 5); i++)  // 5���� ũ�� 5������ �ݺ��ؾ���!!!!!!!!!!!
            {
                // ���ο� ����� �߰��� ��� new ǥ��
                if (i + 1 > beforeRanking.Count)
                {
                    _scoreItem.SendMessage("SwitchOnNewIcon", i);
                    Debug.Log((i + 1) + " Record is Added!");
                    break;
                }
                // ���� ���� ��ϰ� �ٲ� ���� �ִ� ��� new ǥ��
                else if (beforeRanking[i] != afterRanking[i])
                {
                    _scoreItem.SendMessage("SwitchOnNewIcon", i);
                    Debug.Log((i + 1) + " Record is Renewed!");
                    break;
                }
            }
        }

        List<int> finalRanking = ShowRankings(afterRanking);
        int[] param = new int[2];
        Debug.Log("finalRanking Length is " + finalRanking.Count);
        for(int i = 0; i < finalRanking.Count; i++)
        {
            param[0] = i;
            param[1] = finalRanking[i];
            _scoreItem.SendMessage("SetRankings", param);
            Debug.Log((i + 1) + " Record's Ranking is " + finalRanking[i]);
        }
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayButtonClickSound();

        for(int i = 0; i < 5; i++)         // ���� ���� UI ���� �� new �����ܵ� ��� ����
        {
            _scoreItem.SendMessage("SwitchOffNewIcon", i);
        }
        _gameOverUI.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveRecords()
    {
        // BBScoreManager���� ���� ���� �����ͼ� ����
        int curScore = FindObjectOfType<BreakBreakScoreManager>().GetCurScore();
        long unixTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        BB_Records.SaveScoreToCSV(curScore, unixTime);
        _curScore.text = string.Format("{0:#,###}", curScore);
    }

    // ��ŷ ���忡 ���� ��� ǥ��
    public void ShowRecords()
    {
        List<int> records = BB_Records.LoadScoresFromCSV();
        int[] param = new int[2];

        // CSV���Ͽ��� ������ ����� ǥ��
        for (int i = 0; i < Mathf.Clamp(records.Count, records.Count, 5); i++)
        {
            param[0] = i;
            param[1] = records[i];

            //_highScores[i].text = string.Format("{0:#,###}", records[i]);
            _scoreItem.SendMessage("SetHighScores", param);
        }
    }

    // ���� �ִ� 5�� ��� ��ȯ�ϴ� �޼���, ���� ������ �� ���� ȣ��, ���� ��� ������ ���� �� �� �� ȣ��
    public List<int> SaveRankings()
    {
        List<int> records = BB_Records.LoadScoresFromCSV();
        List<int> Rankings = new List<int>();             // ���� ���� �ִ� 5�� ����� ��ȯ�ϴ� �ӽ� ����Ʈ

        for(int i = 0; i < records.Count; i++)
        {
            Rankings.Add(records[i]);
        }

        return Rankings;
    }

    // ��ŷ ���ϴ� �޼���
    public List<int> ShowRankings(List<int> score)
    {
        List<int> ranks = new List<int>();
        int rank = 1;

        for (int i = 0; i < Mathf.Clamp(score.Count, score.Count, 5); i++)
        {
            // ù��° �ε����� 1�� �ڵ� �ο�
            // ù��° �ε����� �ƴϰ� ���� �ε��� ���� ������ ��� ���� ��ŷ �ο�
            if(i > 0 && score[i] < score[i - 1])     // ù��° �ε����� �ƴϰ� ���� �ε��� ������ ���� ��� ���� �ε��� +1�� ��ŷ���� �ο�
            {
                rank = i + 1;
            }
            ranks.Add(rank);
        }

        return ranks;
    }
}
