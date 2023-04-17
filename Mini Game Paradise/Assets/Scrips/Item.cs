using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_spriteRenderer.enabled == true && collision.CompareTag("Player"))
        {
            _spriteRenderer.enabled = false;
            Debug.Log("Got It!");
        }
    }
}
