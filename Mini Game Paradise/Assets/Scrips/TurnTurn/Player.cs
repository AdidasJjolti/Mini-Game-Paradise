using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigid;
    [SerializeField] float _speed = 2;
    [SerializeField] bool _isLeftMoving;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _isLeftMoving = !_isLeftMoving;
        }
    }

    void FixedUpdate()
    {
        float x = _isLeftMoving ? -1 : 1;
        Vector2 dir = new Vector2(x, -1).normalized;
        _rigid.MovePosition(_rigid.position + dir * _speed * Time.fixedDeltaTime);
    }
}
