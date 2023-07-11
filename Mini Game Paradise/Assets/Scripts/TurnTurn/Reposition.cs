using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    BoxCollider2D _collider;
    [SerializeField] BoxCollider2D _leftFailCollider;
    [SerializeField] BoxCollider2D _rightFailCollider;
    [SerializeField] BoxCollider2D _treeCollider;
    [SerializeField] float _distance = 5f;
    [SerializeField] ResetGate _resetter;
    [SerializeField] Transform[] _trees;
    readonly float _offsetX = 1f;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();

        _trees = new Transform[2];
        _trees[0] = transform.GetChild(0);
        _trees[1] = transform.GetChild(1);

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    _trees[i] = transform.GetChild(i);
        //}

        _resetter = transform.GetComponentInParent<ResetGate>();
        transform.position = new Vector2(_resetter.SetPosX(transform.GetSiblingIndex()), transform.position.y);
        GetComponent<BoxCollider2D>().size = new Vector2(1, GetComponent<BoxCollider2D>().size.y);

        SetGap();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("upperTrigger"))
        {
            // 임시로 상하 간격 기입
            float gap = Random.Range(0.1f, 2.0f);
            transform.position = new Vector2(_resetter.SetPosX(transform.GetSiblingIndex()), transform.position.y - transform.parent.childCount * _distance + gap);
            SetGap();
        }
    }

    void SetGap()
    {
        float gap = Random.Range(0.1f, 2.0f);
        _collider.size = new Vector2(_offsetX * 2 + gap + _treeCollider.size.x * 0.5f, _collider.size.y);
        _leftFailCollider.offset = new Vector2((_offsetX * 4 + gap * 0.5f + _treeCollider.size.x * 0.25f) * -1, 0f);
        _rightFailCollider.offset = new Vector2((_offsetX * 4 + gap * 0.5f + _treeCollider.size.x * 0.25f), 0f);

        for (int i = 0; i < _trees.Length; i++)
        {
            if(i % 2 == 0)
            {
                _trees[i].localPosition = new Vector2(-1 * _offsetX, _trees[i].localPosition.y);
                _trees[i].localPosition = new Vector2(_trees[i].localPosition.x - gap / 2, _trees[i].localPosition.y);
            }
            else
            {
                _trees[i].localPosition = new Vector2(_offsetX, _trees[i].localPosition.y);
                _trees[i].localPosition = new Vector2(_trees[i].localPosition.x + gap / 2, _trees[i].localPosition.y);
            }
        }
    }
}
