using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigid;
    [SerializeField] float _speed = 4;
    [SerializeField] bool _isLeftMoving;
    [SerializeField] bool _isStraight;
    [SerializeField] bool _curve;


    bool _isSetPos;
    [SerializeField] Vector2 _startPos;
    [SerializeField] Vector2 _endPos;
    [SerializeField] Vector2 _conPos;
    [SerializeField] float _duration;
    [SerializeField] float _xPos;
    [SerializeField] float _yPos;
    float _curTime = 0f;
    
    bool _isCollided;


    void Awake()
    {
        _isLeftMoving = true;
        _rigid = GetComponent<Rigidbody2D>();
        _isStraight = true;
    }

    void Update()
    {
        if(!_curve && (Input.GetKeyDown(KeyCode.Space)))    //  || Input.GetMouseButtonDown(0))
        {
            _isLeftMoving = !_isLeftMoving;
            _isStraight = false;
        }
    }

    void FixedUpdate()
    {
        if(_isStraight)
        {
            float x = _isLeftMoving ? -1 : 1;
            Vector2 dir = new Vector2(x, -1).normalized;
            _rigid.MovePosition(_rigid.position + dir * _speed * Time.fixedDeltaTime);
        }
        else
        {
            if(!_isSetPos)
            {
                _isSetPos = true;
                SetPos();
            }
            _curTime += Time.fixedDeltaTime;

            float t = _curTime / _duration;
            t = Mathf.Clamp01(t);

            Vector2 position = CalculateBezierCurve(t, _startPos, _conPos, _endPos);
            _rigid.position = position;

            StartCoroutine(Wait());
        }
    }

    private Vector2 CalculateBezierCurve(float t, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        Vector2 point = uu * p0 + 2f * u * t * p1 + tt * p2;
        return point;
    }

    void SetPos()
    {
        _startPos = _rigid.position;
        _endPos = new Vector2(_startPos.x, _startPos.y - _yPos);
        _conPos = new Vector2(_isLeftMoving ? _startPos.x  + _xPos : _startPos.x - _xPos, _startPos.y - _yPos/2);
    }

    IEnumerator Wait()
    {
        if(_curve)
        {
            yield break;
        }
        _curve = true;

        yield return new WaitForSeconds(_duration );
        _curTime = 0f;
        _isStraight = true;
        _isSetPos = false;
        _curve = false;
    }


    // ToDo : 나무와 충돌할 때 점수 오르고 효과 넣기

    public bool GetCollisionState()
    {
        return _isCollided;
    }

    public void SetCollisionState()
    {
        StartCoroutine(CollisionWait());
    }

    IEnumerator CollisionWait()
    {
        _isCollided = true;
        yield return new WaitForSeconds(1f);
        _isCollided = false;
    }
}
