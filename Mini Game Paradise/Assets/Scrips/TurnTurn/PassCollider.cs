using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCollider : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Ελ°ϊ!");
        }
    }
}
