using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _eItemType
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

public class Item : MonoBehaviour
{
    [SerializeField] _eItemType _itemType;

    void Awake()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Got It!");
            ItemPool.Instance.PoolIn(gameObject, _itemType);
        }

        if(collision.CompareTag("upperTrigger"))
        {
            ItemPool.Instance.PoolIn(gameObject, _itemType);
        }
    }

    public _eItemType GetItemType()
    {
        return _itemType;
    }
}
