using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCollider : MonoBehaviour
{
    Player _player;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _player.GetCollisionState() == false)
        {
            Debug.Log("½ÇÆÐ!");
        }
    }
}
