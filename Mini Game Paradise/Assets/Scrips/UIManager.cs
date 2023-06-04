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
        image = imageList.ToArray();  // 제거할 원소 모두 찾은 다음 다시 리스트를 배열로 변환

        // _newIcons 배열 원소를 image에서 채워 넣음
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
        // ToDo : 메인 화면 만들고 구현하기
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

        // ToDo : 이전 상위 5개 기록 저장하기
        List<int> records = BB_Records.LoadScoresFromCSV();
        List<int> beforeRanking = new List<int>();

        if(records.Count == 0)
        {
            SaveRecords();
            ShowRecords();
            _newIcons[0].gameObject.SetActive(true);   // 1위 기록에 new 아이콘 표시
        }
        else          // 보여줄 기록이 1개 이상이면 현재 기록 저장하기 전에 현재 상위 5개 기록을 먼저 저장 후 기록 저장, 기록 표시 실행
        {
            beforeRanking = SaveRankings();

            SaveRecords();
            ShowRecords();

            List<int> afterRanking = SaveRankings();

            // 이전 상위 5개 기록과 저장 후 상위 5개 기록을 비교하여 바뀐 기록이 있으면 해당 기록에 new 아이콘 표시
            for (int i = 0; i < afterRanking.Count; i++)
            {
                // 새로운 기록이 추가된 경우 new 표시
                if (i + 1 > beforeRanking.Count)
                {
                    _newIcons[i].gameObject.SetActive(true);
                    Debug.Log((i + 1) + " Record is Added!");
                    break;
                }
                // 이전 상위 기록과 바뀐 곳이 있는 경우 new 표시
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

        for(int i = 0; i < _newIcons.Length; i++)         // 게임 오버 UI 닫을 때 new 아이콘도 모두 끄기
        {
            _newIcons[i].gameObject.SetActive(false);
        }
        _gameOverUI.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveRecords()
    {
        // BBScoreManager에서 현재 점수 가져와서 저장
        int curScore = FindObjectOfType<BreakBreakScoreManager>().GetCurScore();
        long unixTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        BB_Records.SaveScoreToCSV(curScore, unixTime);
        _curScore.text = string.Format("{0:#,###}", curScore);
    }

    // 랭킹 보드에 상위 기록 표기
    public void ShowRecords()
    {
        List<int> records = BB_Records.LoadScoresFromCSV();

        // CSV파일에서 가져온 기록을 표시
        for (int i = 0; i < records.Count; i++)
        {
            _highScores[i].text = string.Format("{0:#,###}", records[i]);
        }

        // ToDo : 상위 5개 기록 반환하는 메서드 필요할수도....     << SaveRankings 메서드로 따로 구현하고 1개 기능만 하도록 함
    }

    // 상위 최대 5개 기록 반환하는 메서드, 게임 오버할 때 먼저 호출, 현재 기록 저장한 다음 한 번 더 호출
    public List<int> SaveRankings()
    {
        List<int> records = BB_Records.LoadScoresFromCSV();
        List<int> Rankings = new List<int>();             // 현재 상위 최대 5개 기록을 반환하는 임시 리스트

        for(int i = 0; i < records.Count; i++)
        {
            Rankings.Add(records[i]);
        }

        return Rankings;
    }
}
