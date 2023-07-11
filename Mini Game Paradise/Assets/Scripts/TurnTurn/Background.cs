using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("upperTrigger"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - transform.parent.childCount * transform.localScale.y);
        }
    }
}
