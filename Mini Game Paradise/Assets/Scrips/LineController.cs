using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Transform _lineResetter;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);

        if(collision.CompareTag("Line"))
        {
            Debug.Log("ธÞที");
            collision.transform.position = new Vector3 (collision.transform.position.x, _lineResetter.position.y, 0);
        }
    }
}
