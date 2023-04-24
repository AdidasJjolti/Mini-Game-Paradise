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
            Debug.Log("¡Ÿ¿Ã πŸ≤Ó¡ˆ∑’");

            collision.transform.position = new Vector3 (collision.transform.position.x, _lineResetter.position.y, 0);
            collision.GetComponent<SpriteRenderer>().enabled = true;
            collision.GetComponent<BoxCollider2D>().isTrigger = false;
            if (_parent == collision.transform.parent)
            {
                return;
            }
            _parent = collision.transform.parent;
            _parent.GetComponent<CreateItem>().CreateStar();
            Debug.Log("æ∆¿Ã≈€¿Ã ¥ŸΩ√ ª˝∞Â¡ˆ∑’");
        }
    }
}
