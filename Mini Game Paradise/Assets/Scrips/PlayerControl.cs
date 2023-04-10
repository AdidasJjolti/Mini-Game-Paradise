using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rigid;
    bool _isLeftMoving;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _isLeftMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLeftMoving == false)
        {
            _rigid.AddForce(Vector2.right * 5 * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x > 5)
            {
                _rigid.velocity = new Vector2(5, 0);
            }
        }
        else
        {
            _rigid.AddForce(Vector2.right * -5 * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x < -5)
            {
                _rigid.velocity = new Vector2(-5, 0);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isLeftMoving = !_isLeftMoving;
        }
    }


    // 플레이어의 이동 방향이 왼쪽인지 아닌지 반환
    public bool GetLeftMoving()
    {
        if(_rigid.velocity.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
