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
    SpriteRenderer _spriteRenderer;
    [SerializeField] _eItemType _itemType;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(_spriteRenderer.enabled == true && collision.CompareTag("Player"))
        {
            _spriteRenderer.enabled = false;
            Debug.Log("Got It!");
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
