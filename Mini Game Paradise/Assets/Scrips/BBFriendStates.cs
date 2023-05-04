using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _eFriendType
{
    NONE = -1,

    MASKDUDE = 0,
    NINJAFROG,
    PINKMAN,
    VIRTUALGUY,

    MAX
}
public class BBFriendStates : MonoBehaviour
{
    Rigidbody2D _rigid;
    bool _isLeftMoving;
    bool _ableMoving;                // 카메라 안으로 들어올 때부터 움직이기 시작, 생성되자마자 움직여서 떨어지는 것을 방지
    [SerializeField] float _speed;
    SpriteRenderer _renderer;
    [SerializeField] _eFriendType _friendType;
    BBFriendPool _friendPool;

    void Awake()
    {
        _friendPool = GetComponent<BBFriendPool>();
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _isLeftMoving = false;
        _ableMoving = false;
    }


    void Update()
    {
        if (_isLeftMoving == false)
        {
            _renderer.flipX = false;

            if(_ableMoving == false)
            {
                return;
            }

            _rigid.AddForce(Vector2.right * _speed * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x > _speed)
            {
                _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
            }
        }
        else
        {
            _renderer.flipX = true;

            if (_ableMoving == false)
            {
                return;
            }

            _rigid.AddForce(Vector2.right * _speed * -1 * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x < _speed * -1)
            {
                _rigid.velocity = new Vector2(_speed * -1, _rigid.velocity.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("upperTrigger"))
        {
            _friendPool.PoolIn(gameObject, _friendType);
            Debug.Log("친구가 없어지지롱");
        }
        else if(collision.CompareTag("CameraTrigger"))
        {
            _ableMoving = true;
            Debug.Log("친구가 움직일 수 있지롱");
        }
    }

    public void SetLeftMoving(bool _leftMove)
    {
        _isLeftMoving = _leftMove;
    }
}
