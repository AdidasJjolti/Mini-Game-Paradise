using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rigid;
    bool _isLeftMoving;
    [SerializeField] GameObject _lTrigger;
    [SerializeField] GameObject _rTrigger;
    [SerializeField] float _speed;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _isLeftMoving = false;
        _lTrigger.SetActive(!_isLeftMoving);
        _rTrigger.SetActive(_isLeftMoving);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player velocity is " + _rigid.velocity);
        if (_isLeftMoving == false)
        {
            _rigid.AddForce(Vector2.right * _speed * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x > _speed)
            {
                _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
            }
        }
        else
        {
            _rigid.AddForce(Vector2.right * _speed * -1 * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x < _speed * -1)
            {
                _rigid.velocity = new Vector2(_speed * -1, _rigid.velocity.y);
            }
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            _isLeftMoving = !_isLeftMoving;
            _lTrigger.SetActive(!_isLeftMoving);
            _rTrigger.SetActive(_isLeftMoving);
            //transform.position -= new Vector3(0, 2, 0);
        }
        */
    }


    // 플레이어의 이동 방향이 왼쪽인지 아닌지 반환
    public bool GetLeftMoving()
    {
        return _isLeftMoving;
    }
}
