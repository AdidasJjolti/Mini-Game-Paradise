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
    // 친구 상태 관련 변수
    [SerializeField] bool _isLeftMoving;
    [SerializeField] bool _ableMoving;                // 카메라 안으로 들어올 때부터 움직이기 시작, 생성되자마자 움직여서 떨어지는 것을 방지
    [SerializeField] bool _isStunned;
    bool _isGrounded;

    // 친구 상태 제어값
    [SerializeField] float _stunTime;
    [SerializeField] float _speed;
    [SerializeField] float _knockbackPower;

    [SerializeField] _eFriendType _friendType;

    BBFriendPool _friendPool;
    Collider2D _collider;
    Rigidbody2D _rigid;
    SpriteRenderer _renderer;

    Animator _animator;

    void Awake()
    {
        _friendPool = GetComponent<BBFriendPool>();
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _isLeftMoving = false;
        _ableMoving = false;
        _isStunned = false;

        _animator = GetComponent<Animator>();
        _animator.SetBool("isStunned", false);
    }


    void Update()
    {
        if(_isStunned)
        {
            return;
        }

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
        if(_isStunned == true)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }

        if(collision.gameObject.CompareTag("Friend") && _isStunned == false && collision.gameObject.GetComponent<BBFriendStates>().GetFriendState() == false)
        {
            StartCoroutine(Stun(collision, true));
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
                StartCoroutine(Stun(collision, false));
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

    public bool GetLeftMoving()
    {
        return _isLeftMoving;
    }

    public void SetLeftMoving(bool _leftMove)
    {
        _isLeftMoving = _leftMove;
    }

    public bool GetFriendState()
    {
        return _isStunned;
    }

    // 스턴 상태일 때 움직임을 멈추고 거꾸로 된 상태로 변경
    IEnumerator Stun(Collision2D collision, bool isFriend)
    {
        if(_isStunned == true)
        {
            yield break;
        }

        // 스턴 시 처리
        _isStunned = true;
        _ableMoving = false;
        _rigid.velocity = Vector2.zero;
        _renderer.flipY = true;
        _animator.SetBool("isStunned", true);

        // 친구와 충돌하여 스턴 시 넉백 처리 추가
        if (isFriend)
        {
            if(this.transform.position.x > collision.transform.position.x)
            {
                StartCoroutine(KnockBack(1)) ;
            }
            else
            {
                StartCoroutine(KnockBack(-1));
            }
            Debug.Log("Bang!!");
        }

        //Physics2D.IgnoreLayerCollision(7, 7, true);
        //Physics2D.IgnoreLayerCollision(7, 3, true);
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        yield return new WaitForSecondsRealtime(_stunTime);

        // 스턴 해제 후 원상 복귀
        _isStunned = false;
        _ableMoving = true;
        _renderer.flipY = false;
        _animator.SetBool("isStunned", false);
        //Physics2D.IgnoreLayerCollision(7, 7, false);
        //Physics2D.IgnoreLayerCollision(7, 3, false);
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }

    IEnumerator KnockBack(int dir)        // 넉백 방향을 dir으로 받아옴;  1이면 오른쪽, -1이면 왼쪽
    {
        float time = 0;
        float duration = 0.5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            _rigid.AddRelativeForce((transform.up * 2f + transform.right * dir) * _knockbackPower);
        }

        yield return new WaitForSecondsRealtime(duration);

        _rigid.velocity = Vector2.zero;
        Vector2 curPosition = transform.position;
        time = 0;
        duration = _stunTime - duration * 2;

        while(time < duration)
        {
            time += Time.deltaTime;
            transform.position = curPosition;
            _rigid.velocity = Vector2.zero;
        }

        yield return 0;
    }
}
