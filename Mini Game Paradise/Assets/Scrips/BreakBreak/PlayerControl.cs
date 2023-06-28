using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rigid;
    [SerializeField] bool _isLeftMoving;
    [SerializeField] float _speed;
    [SerializeField] bool _isGrounded;

    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Animator _animator;

    [SerializeField] GameObject _leftBGTrigger;
    [SerializeField] GameObject _rightBGTrigger;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _isLeftMoving = false;
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.down, 0.6f);
        Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.red);
        foreach (var hit in hits)
        {
            _isGrounded = false;

            if (hit.transform.CompareTag("Line"))
            {
                _isGrounded = true;
                //Debug.Log("<color=green>_isGrounded = true</color>");
                break;
            }
        }

        _animator.SetBool("isGrounded", _isGrounded);

        if (_isGrounded == false)
        {
            //Debug.Log("<color=red>_isGrounded = false</color>");
        }

        //Debug.Log("Player velocity is " + _rigid.velocity);
        if (_isLeftMoving == false)
        {
            _leftBGTrigger.SetActive(true);
            _rightBGTrigger.SetActive(false);
            _renderer.flipX = false;
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
            gameObject.transform.Translate(Vector2.right * _speed * Time.deltaTime);
            //_rigid.MovePosition(_rigid.position + Vector2.right * _speed * Time.deltaTime);
        }
        else
        {
            _leftBGTrigger.SetActive(false);
            _rightBGTrigger.SetActive(true);
            _renderer.flipX = true;
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
            gameObject.transform.Translate(Vector2.left * _speed * Time.deltaTime);
            //_rigid.MovePosition(_rigid.position + Vector2.left * _speed * Time.deltaTime);
        }

        //Debug.Log($"_rigid.velocity.x = { _rigid.velocity.x}");
    }

    // 플레이어가 땅에 닿아 있는 상태면 true, 아니면 false
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Line"))
    //    {
    //        //Debug.Log("땅에 닿음");
    //        _isGrounded = true;
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag("Friend") && collision.gameObject.GetComponent<BBFriendStates>().GetFriendState() == true)
        //{
        //    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        //}
    }

    public bool GetGrounded()
    {
        return _isGrounded;
    }

    // 플레이어의 이동 방향이 왼쪽인지 아닌지 반환
    public bool GetLeftMoving()
    {
        return _isLeftMoving;
    }

    public void SetLeftMoving(bool direction)
    {
        _isLeftMoving = direction;
    }

    public void SetGrounded(bool grounded)
    {
        _isGrounded = grounded;
    }
}
