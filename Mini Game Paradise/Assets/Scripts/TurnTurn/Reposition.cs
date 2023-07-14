using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    TurnTurnGameManager _gameManager;

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
        _gameManager = FindObjectOfType<TurnTurnGameManager>();

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
            // 게이트 끼리의 좌우 간격, 상하 간격 수정
            transform.position = new Vector2(_resetter.SetPosX(transform.GetSiblingIndex()), transform.position.y - transform.parent.childCount * _distance + GateData.SetVerticalDistance(_gameManager.GetGateCount()));
            //Debug.Log($"기본 간격은 {transform.parent.childCount * _distance}만큼, 추가 간격은 {GateData.SetVerticalDistance(_gameManager.GetGateCount())}");
            SetGap();
            GetComponent<TurnTurnCreateItem>().CreateStar();
        }
    }

    // 나무 사이 간격 수정
    void SetGap()
    {
        float gap = Random.Range(GateData.SetTreeDistance(_gameManager.GetGateCount()).Item1, GateData.SetTreeDistance(_gameManager.GetGateCount()).Item2);
        _collider.size = new Vector2(_offsetX * 2 + gap + _treeCollider.size.x * 0.5f, _collider.size.y);      // 통과 충돌체 크기 수정
        _leftFailCollider.offset = new Vector2((_offsetX * 4 + gap * 0.5f + _treeCollider.size.x * 0.25f) * -1, -0.65f);    // 왼쪽 실패 충돌체 offset 수정
        _rightFailCollider.offset = new Vector2((_offsetX * 4 + gap * 0.5f + _treeCollider.size.x * 0.25f), -0.65f);        // 오른쪽 실패 충돌체 offset 수정

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

        Debug.Log($"왼쪽 나무의 포지션은 {_trees[0].position.x}이고 오른쪽 나무의 포지션은 {_trees[1].position.x}");

        // 나무 한 쪽이 화면 경계에 닿으면 재배치
        if (_trees[0].position.x < -5.0f || _trees[1].position.x > 5.0f)
        {
            SetGap();
        }
    }
}
