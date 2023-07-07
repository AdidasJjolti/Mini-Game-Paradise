using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCollider : MonoBehaviour
{
    Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && _player.GetCollisionState() == false)
        {
            Debug.Log("���!");
        }
    }
}
