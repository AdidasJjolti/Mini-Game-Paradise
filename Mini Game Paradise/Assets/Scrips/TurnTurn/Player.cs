using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigid;
    [SerializeField] float _speed = 4;
    [SerializeField] bool _isLeftMoving;
    [SerializeField] bool _isStraight;


    bool _isSetPos;
    [SerializeField] Vector2 _startPos;
    [SerializeField] Vector2 _endPos;
    [SerializeField] Vector2 _conPos;
    float _duration = 1f;
    float _curTime = 0f;


    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _isStraight = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
        _endPos = new Vector2(_startPos.x, _startPos.y - 3f);
        _conPos = new Vector2(_startPos.x, _startPos.y - 1.5f);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _curTime = 0f;
        _isStraight = true;
        _isSetPos = false;
    }
}
