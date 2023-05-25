using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<SoundManager>();

                if(instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }

    [SerializeField] AudioClip _bgmClip;
    [SerializeField] AudioClip[] _sfxClips;

    AudioSource _bgmPlayer;
    AudioSource _sfxPlayer;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _sfxPlayer = GetComponent<AudioSource>();
        SetBGM();

        if (_bgmPlayer != null)
        {
            _bgmPlayer.Play();
        }
    }

    void SetBGM()
    {
        GameObject child = new GameObject("BGM");
        child.transform.parent = transform;
        _bgmPlayer = child.AddComponent<AudioSource>();
        _bgmPlayer.clip = _bgmClip;

        if(PlayerPrefs.HasKey("BGMVolume"))
        {
            _bgmPlayer.volume = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            _bgmPlayer.volume = 1f;
        }
    }

    public void PlayButtonClickSound()
    {
        _sfxPlayer.PlayOneShot(_sfxClips[0]);
    }

    public void PlayCollectSound()
    {
        _sfxPlayer.PlayOneShot(_sfxClips[1]);
    }

    public void PlayJumpSound()
    {
        _sfxPlayer.PlayOneShot(_sfxClips[2]);
    }

    public void PlayJumpOnFriendSound()
    {
        _sfxPlayer.PlayOneShot(_sfxClips[3]);
    }

    public void SetBGMVolume(float volume)
    {
        _bgmPlayer.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxPlayer.volume = volume;
    }
}
