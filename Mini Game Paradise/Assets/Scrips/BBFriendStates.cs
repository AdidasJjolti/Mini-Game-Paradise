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
    [SerializeField] bool _isLeftMoving;
    [SerializeField] bool _ableMoving;                // 카메라 안으로 들어올 때부터 움직이기 시작, 생성되자마자 움직여서 떨어지는 것을 방지
    [SerializeField] bool _isStunned;
    bool _isGrounded;
    [SerializeField] float _stunTime;
    [SerializeField] float _speed;
    SpriteRenderer _renderer;
    [SerializeField] _eFriendType _friendType;
    BBFriendPool _friendPool;
    Collider2D _collider;

    void Awake()
    {
        _friendPool = GetComponent<BBFriendPool>();
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _isLeftMoving = false;
        _ableMoving = false;
        _isStunned = false;
    }


    void Update()
    {
        if (_isLeftMoving == false)
        {
            _renderer.flipX = false;
            if (_ableMoving == false)
            {
                _rigid.velocity = new Vector2(0, 0);
                return;
            }
            _rigid.AddForce(Vector2.right * _speed * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x > _speed)
            {
                _rigid.velocity = new Vector2(_speed * Time.deltaTime, _rigid.velocity.y);
            }
        }
        else
        {
            _renderer.flipX = true;
            if (_ableMoving == false)
            {
                _rigid.velocity = new Vector2(0, 0);
                return;
            }
            _rigid.AddForce(Vector2.left * _speed * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x < _speed * -1)
            {
                _rigid.velocity = new Vector2(-1 * _speed * Time.deltaTime, _rigid.velocity.y);
            }
        }
    }

    void OnDisable()
    {
        //Debug.Log("친구 사라짐");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("upperTrigger"))
        {
            if(_friendPool == null)
            {
                Destroy(gameObject);
                return;
            }
            _friendPool.PoolIn(gameObject, _friendType);
            //Debug.Log("친구가 없어지지롱");
        }
        else if(collision.CompareTag("CameraTrigger"))
        {
            _ableMoving = true;
            //Debug.Log("친구가 움직일 수 있지롱");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Friend") && _isStunned == false)
        {
            StartCoroutine(Stun(collision));
        }
        else if(collision.gameObject.CompareTag("Player") && _isStunned == false)
        {
            // 플레이어의 충돌 y좌표가 친구 y좌표보다 높으면 친구는 스턴 상태
            if((collision.transform.position.y - transform.position.y) > 0.7f)
            {
                // 친구와 충돌 시 오른쪽에서 발생하면 플레이어 이동 방향을 우측, 왼쪽에서 발생하면 플레이어 이동 방향을 좌측 설정
                if(collision.transform.position.x > transform.position.x)
                {
                    collision.gameObject.GetComponent<PlayerControl>().SetLeftMoving(false);
                }
                else
                {
                    collision.gameObject.GetComponent<PlayerControl>().SetLeftMoving(true);
                }
                StartCoroutine(Stun(collision));
            }
            else
            {
                if(collision.gameObject.GetComponent<PlayerControl>().GetGrounded() == true)
                {
                    BBGameManager BBGameManager = FindObjectOfType<BBGameManager>();
                    BBGameManager.SetGameOver();
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1f);
            if(hit.collider == null)
            {
                if(this.GetLeftMoving() == false)
                {
                    _rigid.velocity = new Vector2(1f, _rigid.velocity.y);
                }
                else
                {
                    _rigid.velocity = new Vector2(1f, _rigid.velocity.y);
                }
                _isGrounded = false;
            }
        }
    }

    public bool GetLeftMoving()
    {
        return _isLeftMoving;
    }

    public void SetLeftMoving(bool _leftMove)
    {
        _isLeftMoving = _leftMove;
    }

    // 스턴 상태일 때 움직임을 멈추고 거꾸로 된 상태로 변경
    IEnumerator Stun(Collision2D collision)
    {
        if(_isStunned == true)
        {
            yield break;
        }

        _isStunned = true;
        _ableMoving = false;
        _renderer.flipY = true;
        Physics2D.IgnoreCollision(_collider, collision.gameObject.GetComponent<Collider2D>());
        yield return new WaitForSecondsRealtime(_stunTime);
        _isStunned = false;
        _ableMoving = true;
        _renderer.flipY = false;
        Physics2D.IgnoreCollision(_collider, collision.gameObject.GetComponent<Collider2D>(),false);
    }
}
