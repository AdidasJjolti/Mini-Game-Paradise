using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCollider : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("½ÇÆÐ!");
        }
    }
}
