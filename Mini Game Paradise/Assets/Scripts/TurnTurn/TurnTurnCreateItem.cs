using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTurnCreateItem : MonoBehaviour
{
    static bool _itemSkipLine;   // ù ����Ʈ������ ��Ÿ�� �������� �ʱ� ���� static bool ����

    bool _isCreating;
    bool _isReset;           // upperTrigger�� ������ ��ġ�� �ٲ�� true

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

        // ������ ���� Ȯ�� 66%�� ����
        float chance = Random.Range(0f, 1f);
        if (chance > 0.66f)
        {
            _isCreating = false;
            yield break;
        }

        // ���� Ȯ�� ���� ������ ������ ����
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
