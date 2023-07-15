using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTurnCreateItem : MonoBehaviour
{
    static bool _itemSkipLine;   // 첫 게이트에서는 스타를 생성하지 않기 위한 static bool 변수

    bool _isCreating;
    bool _isReset;           // upperTrigger를 만나서 위치가 바뀌면 true

    TurnTurnItemPool _itemPool;

    void Awake()
    {
        if (_itemPool == null)
        {
            _itemPool = FindObjectOfType<TurnTurnItemPool>();
        }

        if (_itemSkipLine == false)
        {
            _itemSkipLine = true;
            return;
        }

        if (this.transform.name == "Gate" && _isReset == false)
        {
            return;
        }
        else
        {
            CreateStar();
        }
    }

    public void CreateStar()
    {
        StartCoroutine("CreatingStar");
    }

    IEnumerator CreatingStar()
    {
        if (_isCreating)
        {
            yield break;
        }

        _isCreating = true;

        // 아이템 생성 확률 66%로 설정
        float chance = Random.Range(0f, 1f);
        if (chance > 0.66f)
        {
            _isCreating = false;
            yield break;
        }

        // 생성 확률 내에 들어오면 아이템 생성
        int itemType = Random.Range(0, 9);
        _eTurnTurnItemType type = (_eTurnTurnItemType)itemType;

        GameObject obj = null;
        switch (type)
        {
            case _eTurnTurnItemType.YELLOW_SINGLE:
                obj = _itemPool.PoolOut("YELLOW_SINGLE");
                break;
            case _eTurnTurnItemType.GREEN_SINGLE:
                obj = _itemPool.PoolOut("GREEN_SINGLE");
                break;
            case _eTurnTurnItemType.ORANGE_SINGLE:
                obj = _itemPool.PoolOut("ORANGE_SINGLE");
                break;

            case _eTurnTurnItemType.YELLOW_DOUBLE:
                obj = _itemPool.PoolOut("YELLOW_DOUBLE");
                break;
            case _eTurnTurnItemType.GREEN_DOUBLE:
                obj = _itemPool.PoolOut("GREEN_DOUBLE");
                break;
            case _eTurnTurnItemType.ORANGE_DOUBLE:
                obj = _itemPool.PoolOut("ORANGE_DOUBLE");
                break;

            case _eTurnTurnItemType.YELLOW_TRIPLE:
                obj = _itemPool.PoolOut("YELLOW_TRIPLE");
                break;
            case _eTurnTurnItemType.GREEN_TRIPLE:
                obj = _itemPool.PoolOut("GREEN_TRIPLE");
                break;
            case _eTurnTurnItemType.ORANGE_TRIPLE:
                obj = _itemPool.PoolOut("ORANGE_TRIPLE");
                break;
        }

        if (obj != null)
        {
            GameObject ob = obj;
            ob.transform.parent = transform;
            //ob.transform.position = transform.parent.position + new Vector3(0, 0.5f, 0);
            SetStarPosition(ob);
            //Debug.Log($"block : {_blocks[index].position}, obj : {ob.transform.position}");
        }
        else
        {
            //Debug.LogError($"failed : {type.ToString()} / obj : {obj}");
        }

        _isCreating = false;

        yield return null;
    }

    public void SetStarPosition(GameObject star)
    {
        float x = Random.Range(-1.5f, 1.5f);
        float y = Random.Range(-2.0f, 2.0f);

        star.transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);

        if(star.transform.position.x < -5.0f)
        {
            star.transform.position = new Vector3(- 5.0f, transform.position.y + y, transform.position.z);
        }
        else if (star.transform.position.x > 5.0f)
        {
            star.transform.position = new Vector3(5.0f, transform.position.y + y, transform.position.z);
        }
    }
}
