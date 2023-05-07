using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rigid;
    bool _isLeftMoving;
    [SerializeField] float _speed;
    bool _isGrounded;
    [SerializeField] SpriteRenderer _renderer;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _isLeftMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player velocity is " + _rigid.velocity);
        if (_isLeftMoving == false)
        {
            _renderer.flipX = false;
            _rigid.AddForce(Vector2.right * _speed * Time.deltaTime, ForceMode2D.Force);
            if (_rigid.velocity.x > _speed)
            {
                _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
            }
        }
        else
        {
            _renderer.flipX = true;
            _rigid.AddForce(Vector2.right * _speed * -1 * Time.deltaTime, ForceMode2D.Force);
            if (_rigid.velocity.x < _speed * -1)
            {
                _rigid.velocity = new Vector2(-1 * _speed, _rigid.velocity.y);
            }
        }
    }

    // �÷��̾ ���� ��� �ִ� ���¸� true, �ƴϸ� false
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Line"))
        {
            //Debug.Log("���� ����");
            _isGrounded = true;
        }
    }

    public bool GetGrounded()
    {
        return _isGrounded;
    }

    public void SetGrounded(bool isGrounded)
    {
        Debug.Log("������ ������");
        _isGrounded = isGrounded;
    }

    // �÷��̾��� �̵� ������ �������� �ƴ��� ��ȯ
    public bool GetLeftMoving()
    {
        return _isLeftMoving;
    }

    public void SetLeftMoving(bool direction)
    {
        _isLeftMoving = direction;
    }
}
