using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Transform _lineResetter;
    [SerializeField] PlayerControl _playerControl;
    Transform _parent;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Line") && _playerControl.GetGrounded() == true)
        {
            //Debug.Log("ÁÙÀÌ ¹Ù²îÁö·Õ");

            collision.transform.position = new Vector3 (collision.transform.position.x, _lineResetter.position.y, 0);
            collision.GetComponent<SpriteRenderer>().enabled = true;
            collision.GetComponent<Collider2D>().isTrigger = false;
            if (_parent == collision.transform.parent)
            {
                return;
            }
            _parent = collision.transform.parent;
            _parent.GetComponent<BBCreateItem>().CreateStar();
            //Debug.Log("¾ÆÀÌÅÛÀÌ ´Ù½Ã »ı°åÁö·Õ");
            _parent.GetComponent<BBCreateFriend>().CreateFriend();
            //Debug.Log("Ä£±¸°¡ ´Ù½Ã »ı°åÁö·Õ");
        }
    }
}
