using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Transform _lineResetter;
    [SerializeField] PlayerControl _playerControl;

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);

        if(collision.CompareTag("Line") && _playerControl.GetGrounded() == true)
        {
            Debug.Log("ธÞที");
            collision.transform.position = new Vector3 (collision.transform.position.x, _lineResetter.position.y, 0);
            collision.GetComponent<SpriteRenderer>().enabled = true;
            collision.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
