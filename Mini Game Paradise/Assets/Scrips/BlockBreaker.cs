using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    //void RemoveObserver(IObserver observer);          // 필요 없을거 같아서 주석 처리
    void NotifyObservers();
}

public class BlockBreaker : MonoBehaviour, ISubject
{

    List<IObserver> observers = new List<IObserver>();

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

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    //public void RemoveObserver(IObserver observer)
    //{
    //    observers.Remove(observer);
    //}

    public void NotifyObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.ScoreUpdate("BlockBreaker");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(_isClicked == true)
        {
            if (collision.transform.CompareTag("Line") && _playerControl.GetGrounded())          // Line 태그에 닿았고 플레이어가 땅에 닿은 상태일 때 실행
            {
                _playerRigidbody2D.velocity = new Vector2(1f, _playerRigidbody2D.velocity.y);
                collision.GetComponent<SpriteRenderer>().enabled = false;
                collision.GetComponent<Collider2D>().isTrigger = true;
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


    // 프레임이 끝날 때까지 땅에 닿지 않은 상태를 유지, 여러 블럭이 동시에 BlockBreaker와 닿기 때문에 이렇게 하지 않으면 첫번째 블럭만 깨지고 나머지는 깨지지 않는 현상 발생
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
        NotifyObservers();
    }
}
