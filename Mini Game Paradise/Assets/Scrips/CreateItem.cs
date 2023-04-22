using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    Transform[] _blocks;
    void Awake()
    {
        _blocks = transform.GetComponentsInChildren<Transform>();
        CreateStar();
    }

    void CreateStar()
    {
        // 아이템 생성 확률 66%로 설정
        float chance = Random.Range(0f, 1f);
        if(chance > 0.66f)
        {
            return;
        }

        // 생성 확률 내에 들어오면 아이템 생성
        int itemType = Random.Range(0, 9);
        _eItemType type = (_eItemType)itemType;

        GameObject obj = null;
        int index = Random.Range(0, _blocks.Length);
        switch (type)
        {
            case _eItemType.YELLOW_SINGLE:
                obj = ItemPool.Instance.PoolOut("YELLOW_SINGLE");
                break;
            case _eItemType.GREEN_SINGLE:
                obj = ItemPool.Instance.PoolOut("GREEN_SINGLE");
                break;
            case _eItemType.ORANGE_SINGLE:
                obj = ItemPool.Instance.PoolOut("ORANGE_SINGLE");
                break;

            case _eItemType.YELLOW_DOUBLE:
                obj = ItemPool.Instance.PoolOut("YELLOW_DOUBLE");
                break;
            case _eItemType.GREEN_DOUBLE:
                obj = ItemPool.Instance.PoolOut("GREEN_DOUBLE");
                break;
            case _eItemType.ORANGE_DOUBLE:
                obj = ItemPool.Instance.PoolOut("ORANGE_DOUBLE");
                break;

            case _eItemType.YELLOW_TRIPLE:
                obj = ItemPool.Instance.PoolOut("YELLOW_TRIPLE");
                break;
            case _eItemType.GREEN_TRIPLE:
                obj = ItemPool.Instance.PoolOut("GREEN_TRIPLE");
                break;
            case _eItemType.ORANGE_TRIPLE:
                obj = ItemPool.Instance.PoolOut("ORANGE_TRIPLE");
                break;
        }

        if(obj != null)
        {
            obj.transform.position = new Vector3(_blocks[index].position.x, _blocks[index].position.y + 0.5f, _blocks[index].position.z);
        }
        else
        {
            Debug.LogError($"failed : {type.ToString()} / obj : {obj}");
        }
    }
}
