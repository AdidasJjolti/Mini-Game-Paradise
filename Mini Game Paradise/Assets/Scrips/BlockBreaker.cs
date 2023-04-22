using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreaker : MonoBehaviour
{
    //[SerializeField] bool _isGrounded;
    [SerializeField] bool _isFirstTouch;
    [SerializeField] CapsuleCollider2D _playerCollider;
    [SerializeField] Rigidbody2D _playerRigidbody2D;
    [SerializeField] PlayerControl _playerControl;
    bool _isClicked;


    void Awake()
    {
        _isClicked = false;
        _isFirstTouch = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isClicked = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(_isClicked == true)
        {
            if (collision.transform.CompareTag("Line") && _playerControl.GetGrounded())
            {
                _playerRigidbody2D.velocity = new Vector2(0.5f, _playerRigidbody2D.velocity.y);
                collision.GetComponent<SpriteRenderer>().enabled = false;
                collision.GetComponent<BoxCollider2D>().isTrigger = true;
                StartCoroutine("SetPlayerState");
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector3.down, 1f);
            Debug.DrawRay(transform.position, Vector3.down, Color.green, 2f);
            foreach (var item in hit)
            {
                if (item.transform.CompareTag("Line"))
                {
                    _playerCollider.isTrigger = false;
                    _isClicked = false;
                }
            }
        }
    }

    private bool onCoroutine;
    IEnumerator SetPlayerState()
    {
        if(onCoroutine)
        {
            yield break;
        }
        onCoroutine = true;

        yield return new WaitForEndOfFrame();
        _playerControl.SetGrounded(false);
        onCoroutine = false;
    }
}
