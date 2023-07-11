using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBreakBackground : MonoBehaviour
{
    PlayerControl _player;

    void Awake()
    {
        _player = FindObjectOfType<PlayerControl>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("upperBackgroundTrigger"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 19.9584f);
        }
        else if (collision.CompareTag("leftBackgroundTrigger"))
        {
            Debug.Log("leftBGTrigger was collided with " + transform.name);
            transform.position = new Vector2(transform.position.x + 16.8f, transform.position.y);
        }
        else if(collision.CompareTag("rightBackgroundTrigger"))
        {
            transform.position = new Vector2(transform.position.x - 16.8f, transform.position.y);
        }
    }
}
