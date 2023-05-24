using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _settingsUI;
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
        _settingsUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickResumeButton()
    {
        Time.timeScale = 1;
        _settingsUI.SetActive(false);
    }

    public void BGMOnValueChanged(float bgmVolume)
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
    }

    public void SFXOnValueChanged(float sfxVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }
}
