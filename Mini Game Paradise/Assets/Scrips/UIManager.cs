using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _SettingsUI;
    [SerializeField] GameObject _GameOverUI;
    [SerializeField] Slider _BGMSlider;
    [SerializeField] Slider _SFXSlider;

    void Awake()
    {
        if(PlayerPrefs.HasKey("BGMVolume"))
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
    }

    public void OnClickSettingsButton()
    {
        _SettingsUI.SetActive(true);
        SoundManager.Instance.PlayButtonClickSound();
        Time.timeScale = 0;
    }

    public void OnClickResumeButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayButtonClickSound();
        _SettingsUI.SetActive(false);
    }

    public void OnClickMainButton()
    {
        // ToDo : 구현 필요
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
        _GameOverUI.SetActive(true);
        //기록 뿌리기
        SaveRecords();      // 현재 기록 저장, 현재 기록은 BBScoreManager에서 가져오기
        ShowRecords();      // 내림차순으로 상위 5개 기록을 순위표에 차례대로 표시
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayButtonClickSound();
        _GameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveRecords()
    {
        // csv 파일에 기록 저장하기
        // 1. BBScoreManager에서 현재 기록 가져오기 : GetCurScore 호출
        // 2. GetCurScore로 호출된 int값을 BB_Records.SaveIntToCSV를 사용하여 저장
    }

    public void ShowRecords()
    {
        // csv 파일에서 기록 가져오기
        // 1. BB_Records.ReadIntFromCSV를 사용하여 데이터 불러오기, 인트 리스트로 불러옴
        // 2. 첫번째 값부터 다섯번째 값까지 순서대로 1위 ~ 5위 기록 표시
        // 3. 불러올 값이 없는 경우 -로 표시
    }
}
