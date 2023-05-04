using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockResetter : MonoBehaviour
{
    [SerializeField] PlayerControl _player;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Line") || collision.CompareTag("Item") || collision.CompareTag("Friend"))
        {
            if(_player.GetLeftMoving() == true)
            {
                collision.transform.position -= new Vector3(8, 0, 0);
            }
            else
            {
                collision.transform.position += new Vector3(8, 0, 0);
            }
        }
    }
}
