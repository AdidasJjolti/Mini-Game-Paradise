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

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _isLeftMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.down, 1f);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        foreach (var hit in hits)
        {
            _isGrounded = false;

            if (hit.transform.CompareTag("Line"))
            {
                _isGrounded = true;
                Debug.Log("<color=green>_isGrounded = true</color>");
                break;
            }
        }

        if(_isGrounded == false)
        {
            Debug.Log("<color=red>_isGrounded = false</color>");
        }

        //Debug.Log("Player velocity is " + _rigid.velocity);
        if (_isLeftMoving == false)
        {
            _renderer.flipX = false;
            gameObject.transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
        else
        {
            _renderer.flipX = true;
            gameObject.transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }

        //Debug.Log($"_rigid.velocity.x = { _rigid.velocity.x}");
    }

    // �÷��̾ ���� ��� �ִ� ���¸� true, �ƴϸ� false
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Line"))
    //    {
    //        //Debug.Log("���� ����");
    //        _isGrounded = true;
    //    }
    //}

    public bool GetGrounded()
    {
        return _isGrounded;
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
