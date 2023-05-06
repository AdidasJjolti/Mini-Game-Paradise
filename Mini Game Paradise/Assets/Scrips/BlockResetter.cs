using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockResetter : MonoBehaviour
{
    [SerializeField] PlayerControl _player;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Line") || collision.CompareTag("Item"))
        {
            if(transform.name.Equals("RightTrigger"))
            {
                collision.transform.position -= new Vector3(8, 0, 0);
            }
            else if(transform.name.Equals("LeftTrigger"))
            {
                collision.transform.position += new Vector3(8, 0, 0);
            }
        }
        else if(collision.CompareTag("Friend"))
        {
            if (transform.name.Equals("RightTrigger"))
            {
                collision.transform.position = new Vector3(_player.transform.position.x - 2f, collision.transform.position.y, collision.transform.position.z);
            }
            else if (transform.name.Equals("LeftTrigger"))
            {
                collision.transform.position = new Vector3(_player.transform.position.x + 2f, collision.transform.position.y, collision.transform.position.z);
            }
        }
    }
}
