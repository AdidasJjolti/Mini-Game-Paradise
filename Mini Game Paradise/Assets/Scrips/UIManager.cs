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
    [SerializeField] TextMeshProUGUI[] _highScores;
    [SerializeField] Image[] _newIcons;


    [SerializeField] Slider _BGMSlider;
    [SerializeField] Slider _SFXSlider;



    void Awake()
    {
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

        if(_highScores.Length < 5)
        {
            for(int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "FirstScore")
                            {
                                _highScores[0] = text;
                            }
                        }
                        break;
                    case 1:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "SecondScore")
                            {
                                _highScores[1] = text;
                            }
                        }
                        break;
                    case 2:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "ThirdScore")
                            {
                                _highScores[2] = text;
                            }
                        }
                        break;
                    case 3:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "FourthScore")
                            {
                                _highScores[3] = text;
                            }
                        }
                        break;
                    case 4:
                        foreach (var text in texts)
                        {
                            if (text.transform.name == "FifthScore")
                            {
                                _highScores[4] = text;
                            }
                        }
                        break;
                }
            }
        }

        Image[] image = FindObjectsOfType<Image>(true);
        List<Image> imageList = new List<Image>(image);
        for(int i = 0; i < imageList.Count; i++)
        {
            if(!imageList[i].CompareTag("New"))
            {
                imageList.Remove(imageList[i]);
            }
        }
        image = imageList.ToArray();  // ������ ���� ��� ã�� ���� �ٽ� ����Ʈ�� �迭�� ��ȯ

        // _newIcons �迭 ���Ҹ� image���� ä�� ����
        if(_newIcons.Length < 5)
        {
            for(int i = 0; i < 5; i++)
            {
                _newIcons[i] = image[i];
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

        if(records.Count == 0)
        {
            SaveRecords();
            ShowRecords();
            _newIcons[0].gameObject.SetActive(true);   // 1�� ��Ͽ� new ������ ǥ��
        }
        else          // ������ ����� 1�� �̻��̸� ���� ��� �����ϱ� ���� ���� ���� 5�� ����� ���� ���� �� ��� ����, ��� ǥ�� ����
        {
            beforeRanking = SaveRankings();

            SaveRecords();
            ShowRecords();

            List<int> afterRanking = SaveRankings();

            // ���� ���� 5�� ��ϰ� ���� �� ���� 5�� ����� ���Ͽ� �ٲ� ����� ������ �ش� ��Ͽ� new ������ ǥ��
            for (int i = 0; i < afterRanking.Count; i++)
            {
                // ���ο� ����� �߰��� ��� new ǥ��
                if (i + 1 > beforeRanking.Count)
                {
                    _newIcons[i].gameObject.SetActive(true);
                    Debug.Log((i + 1) + " Record is Added!");
                    break;
                }
                // ���� ���� ��ϰ� �ٲ� ���� �ִ� ��� new ǥ��
                else if (beforeRanking[i] != afterRanking[i])
                {
                    _newIcons[i].gameObject.SetActive(true);
                    Debug.Log((i + 1) + " Record is Renewed!");
                    break;
                }
            }
        }
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayButtonClickSound();

        for(int i = 0; i < _newIcons.Length; i++)         // ���� ���� UI ���� �� new �����ܵ� ��� ����
        {
            _newIcons[i].gameObject.SetActive(false);
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

        // CSV���Ͽ��� ������ ����� ǥ��
        for (int i = 0; i < records.Count; i++)
        {
            _highScores[i].text = string.Format("{0:#,###}", records[i]);
        }

        // ToDo : ���� 5�� ��� ��ȯ�ϴ� �޼��� �ʿ��Ҽ���....     << SaveRankings �޼���� ���� �����ϰ� 1�� ��ɸ� �ϵ��� ��
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
}
