using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _eTurnTurnItemType
{
    NONE = -1,

    YELLOW_SINGLE = 0,
    GREEN_SINGLE,
    ORANGE_SINGLE,

    YELLOW_DOUBLE = 3,
    GREEN_DOUBLE,
    ORANGE_DOUBLE,

    YELLOW_TRIPLE = 6,
    GREEN_TRIPLE,
    ORANGE_TRIPLE,

    MAX
}

public class TurnTurnItem : MonoBehaviour
{
    [SerializeField] _eTurnTurnItemType _itemType;
    TurnTurnScoreManager _scoreManager;
    TurnTurnItemPool _itemPool;


    void OnEnable()
    {
        if (_scoreManager == null)
        {
            _scoreManager = FindObjectOfType<TurnTurnScoreManager>();
        }

        if (_itemPool == null)
        {
            _itemPool = FindObjectOfType<TurnTurnItemPool>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("Got It!");
            _scoreManager.SendMessage("ItemScoreUpdate", _itemType);
            SoundManager.Instance.PlayCollectSound();
            _itemPool.PoolIn(gameObject, _itemType);
        }

        if (collision.CompareTag("upperTrigger"))
        {
            _itemPool.PoolIn(gameObject, _itemType);
            //Debug.Log("아이템이 없어지지롱");
        }
    }

    public _eTurnTurnItemType GetItemType()
    {
        return _itemType;
    }
}
