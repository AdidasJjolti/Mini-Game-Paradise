using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D _rigid;
    bool _isLeftMoving;
    [SerializeField] GameObject _lTrigger;
    [SerializeField] GameObject _rTrigger;

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
        if (_isLeftMoving == false)
        {
            _rigid.AddForce(Vector2.right * 5 * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x > 5)
            {
                _rigid.velocity = new Vector2(5, 0);
            }
        }
        else
        {
            _rigid.AddForce(Vector2.right * -5 * Time.deltaTime, ForceMode2D.Impulse);
            if (_rigid.velocity.x < -5)
            {
                _rigid.velocity = new Vector2(-5, 0);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isLeftMoving = !_isLeftMoving;
            _lTrigger.SetActive(!_isLeftMoving);
            _rTrigger.SetActive(_isLeftMoving);
            //transform.position -= new Vector3(0, 2, 0);
        }
    }


    // �÷��̾��� �̵� ������ �������� �ƴ��� ��ȯ
    public bool GetLeftMoving()
    {
        return _isLeftMoving;
    }
}
