using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollider : MonoBehaviour
{
    Player _player;

    void Awake()
    {
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
            }
        }
    }
}
