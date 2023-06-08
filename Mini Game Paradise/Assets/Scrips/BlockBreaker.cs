using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] bool _isClicked;


    void Awake()
    {
        _isClicked = false;
        _isFirstTouch = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
             _isClicked = true;
        }
        //Debug.Log($"<color=aqua>_isClicked = {_isClicked}</color>");
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
            Debug.Log("땅 뚫었음");
            if (collision.transform.CompareTag("Line") && _playerControl.GetGrounded())          // Line 태그에 닿았고 플레이어가 땅에 닿은 상태일 때 실행
            {
                _playerControl.transform.Translate(Vector2.down * .5f * Time.deltaTime);
                StartCoroutine(SetPlayerState());
                StartCoroutine(SetBlockActive(collision));
            }
        }

        if (collision.CompareTag("Line"))
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector3.down, 0.1f);
            //Debug.DrawRay(transform.position, Vector3.down, Color.green, 2f);
            foreach (var item in hit)
            {
                if (item.transform.CompareTag("Line"))
                {
                    _playerCollider.isTrigger = false;
                }
            }
        }
    }

    // _isGrounded = false 상태에서 땅에 닿을 때 실행
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector3.down, 0.1f);
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
        SoundManager.Instance.PlayJumpSound();         // 여러 블럭이 깨지면서 효과음이 여러번 불리는 것을 코루틴으로 방지

        yield return new WaitForEndOfFrame();
        _playerControl.SetGrounded(false);
        _isClicked = false;
        onCoroutine = false;
        NotifyObservers();
    }

    IEnumerator SetBlockActive(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);

        collision.gameObject.SetActive(true);
        collision.GetComponent<SpriteRenderer>().enabled = false;
        collision.GetComponent<Collider2D>().isTrigger = true;
    }
}
