using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollider : MonoBehaviour
{
    Player _player;
    TurnTurnScoreManager _scoreManager;

    void Awake()
    {
        _scoreManager = FindObjectOfType<TurnTurnScoreManager>();
        _player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(_player.GetCollisionState() == false)
            {
                Debug.Log("나무에 닿았어");
                _player.SetCollisionState();
                if (!SoundManager.Instance.GetComponent<AudioSource>().isPlaying)
                {
                    SoundManager.Instance.PlayJumpSound();
                }
                _scoreManager.ScoreUpdate("Tree");
            }
        }
    }
}
