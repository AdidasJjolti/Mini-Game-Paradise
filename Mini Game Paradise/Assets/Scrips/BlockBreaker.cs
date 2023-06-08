using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    //void RemoveObserver(IObserver observer);          // �ʿ� ������ ���Ƽ� �ּ� ó��
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
            Debug.Log("�� �վ���");
            if (collision.transform.CompareTag("Line") && _playerControl.GetGrounded())          // Line �±׿� ��Ұ� �÷��̾ ���� ���� ������ �� ����
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

    // _isGrounded = false ���¿��� ���� ���� �� ����
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


    // �������� ���� ������ ���� ���� ���� ���¸� ����, ���� ���� ���ÿ� BlockBreaker�� ��� ������ �̷��� ���� ������ ù��° ���� ������ �������� ������ �ʴ� ���� �߻�
    private bool onCoroutine;
    IEnumerator SetPlayerState()
    {
        if(onCoroutine)
        {
            yield break;
        }
        onCoroutine = true;
        SoundManager.Instance.PlayJumpSound();         // ���� ���� �����鼭 ȿ������ ������ �Ҹ��� ���� �ڷ�ƾ���� ����

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
