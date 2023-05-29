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
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlayButtonClickSound();
        _GameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
