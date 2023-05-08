using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBCreateItem : MonoBehaviour
{
    static bool _itemSkipLine;
    Transform[] _blocks;
    bool _isCreating;

    void Awake()
    {
        var tempArray = transform.GetComponentsInChildren<Transform>();
        _blocks = new Transform[tempArray.Length - 1];     // 나 자신을 제외한 임시 배열의 길이 - 1만큼 _blocks 배열의 크기가 결정됨
        int index = 0;                                     // 나 자신을 제외한 _blocks 배열의 크기만큼만 증가
        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] == this.transform)
            {
                continue;
            }
            _blocks[index] = tempArray[i];
            index++;                                       // 최소 1회는 continue가 실행되여 index가 i보다 1 더 작음
        }

        if (_itemSkipLine == false)
        {
            _itemSkipLine = true;
            return;
        }

        CreateStar();
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

        if (obj != null)
        {
            GameObject ob = obj;
            ob.transform.parent = _blocks[index];
            ob.transform.position = _blocks[index].position + new Vector3(0, 0.5f, 0);
            //Debug.Log($"block : {_blocks[index].position}, obj : {ob.transform.position}");
        }
        else
        {
            //Debug.LogError($"failed : {type.ToString()} / obj : {obj}");
        }

        _isCreating = false;

        yield return null;
    }
}
