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
    // ģ�� ���� ���� ����
    [SerializeField] bool _isLeftMoving;
    [SerializeField] bool _ableMoving;                // ī�޶� ������ ���� ������ �����̱� ����, �������ڸ��� �������� �������� ���� ����
    [SerializeField] bool _isStunned;
    bool _isGrounded;

    // ģ�� ���� ���
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
        //Debug.Log("ģ�� �����");
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
            //Debug.Log("ģ���� ����������");
        }
        else if(collision.CompareTag("CameraTrigger"))
        {
            _ableMoving = true;
            //Debug.Log("ģ���� ������ �� ������");
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
            // �÷��̾��� �浹 y��ǥ�� ģ�� y��ǥ���� ������ ģ���� ���� ����
            if((collision.transform.position.y - transform.position.y) > 0.7f)
            {
                // ģ���� �浹 �� �����ʿ��� �߻��ϸ� �÷��̾� �̵� ������ ����, ���ʿ��� �߻��ϸ� �÷��̾� �̵� ������ ���� ����
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

    // ���� ������ �� �������� ���߰� �Ųٷ� �� ���·� ����
    IEnumerator Stun(Collision2D collision, bool isFriend)
    {
        if(_isStunned == true)
        {
            yield break;
        }

        // ���� �� ó��
        _isStunned = true;
        _ableMoving = false;
        _rigid.velocity = Vector2.zero;
        _renderer.flipY = true;
        _animator.SetBool("isStunned", true);

        // ģ���� �浹�Ͽ� ���� �� �˹� ó�� �߰�
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

        // ���� ���� �� ���� ����
        _isStunned = false;
        _ableMoving = true;
        _renderer.flipY = false;
        _animator.SetBool("isStunned", false);
        //Physics2D.IgnoreLayerCollision(7, 7, false);
        //Physics2D.IgnoreLayerCollision(7, 3, false);
        Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }

    IEnumerator KnockBack(int dir)        // �˹� ������ dir���� �޾ƿ�;  1�̸� ������, -1�̸� ����
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
