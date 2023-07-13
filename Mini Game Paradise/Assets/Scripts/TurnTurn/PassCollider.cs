using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCollider : MonoBehaviour
{
    Player _player;
    TurnTurnScoreManager _scoreManager;
    TurnTurnGameManager _gameManager;

    void Awake()
    {
        _gameManager = FindObjectOfType<TurnTurnGameManager>();
        _scoreManager = FindObjectOfType<TurnTurnScoreManager>();
        _player = FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && _player.GetCollisionState() == false)
        {
            //Debug.Log("Åë°ú!");
            _gameManager.CountGates();
            if (!SoundManager.Instance.GetComponent<AudioSource>().isPlaying)
            {
                SoundManager.Instance.PlayJumpSound();
            }
            _scoreManager.ScoreUpdate("Pass");
        }
    }
}
