using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    [SerializeField] float _distance = 5f;
    [SerializeField] float _minX = 1f;
    [SerializeField] float _maxX = 2f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("upperTrigger"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 5 * _distance);
        }
    }
}
