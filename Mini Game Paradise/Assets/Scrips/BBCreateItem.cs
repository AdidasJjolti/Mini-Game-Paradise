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
        _blocks = new Transform[tempArray.Length - 1];     // �� �ڽ��� ������ �ӽ� �迭�� ���� - 1��ŭ _blocks �迭�� ũ�Ⱑ ������
        int index = 0;                                     // �� �ڽ��� ������ _blocks �迭�� ũ�⸸ŭ�� ����
        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] == this.transform)
            {
                continue;
            }
            _blocks[index] = tempArray[i];
            index++;                                       // �ּ� 1ȸ�� continue�� ����ǿ� index�� i���� 1 �� ����
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

        // ������ ���� Ȯ�� 66%�� ����
        float chance = Random.Range(0f, 1f);
        if (chance > 0.66f)
        {
            _isCreating = false;
            yield break;
        }

        // ���� Ȯ�� ���� ������ ������ ����
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
