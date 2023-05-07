using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BBCreateFriend : MonoBehaviour
{
    static bool _friendSkipLine;
    Transform[] _blocks;
    bool _isCreating;
    BBFriendPool _friendPool;

    void Awake()
    {
        if(_friendPool == null)
        {
            _friendPool = FindObjectOfType<BBFriendPool>();
        }

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

        if (_friendSkipLine == false)
        {
            _friendSkipLine = true;
            return;
        }

        CreateFriend();
    }

    public void CreateFriend()
    {
        StartCoroutine("CreatingFriend");
    }

    IEnumerator CreatingFriend()
    {
        if (_isCreating)
        {
            yield break;
        }

        _isCreating = true;

        // ģ�� ���� Ȯ�� 66%�� ����
        float chance = Random.Range(0f, 1f);
        if (chance > 0.66f)
        {
            _isCreating = false;
            yield break;
        }

        // ���� Ȯ�� ���� ������ ģ�� ����
        int friendType = Random.Range(0, 4);
        _eFriendType type = (_eFriendType)friendType;

        GameObject obj = null;
        int index = Random.Range(0, _blocks.Length);
        switch (type)
        {
            case _eFriendType.MASKDUDE:
                obj = _friendPool.PoolOut("MASKDUDE");
                break;
            case _eFriendType.NINJAFROG:
                obj = _friendPool.PoolOut("NINJAFROG");
                break;
            case _eFriendType.PINKMAN:
                obj = _friendPool.PoolOut("PINKMAN");
                break;
            case _eFriendType.VIRTUALGUY:
                obj = _friendPool.PoolOut("VIRTUALGUY");
                break;
        }

        if (obj != null)
        {
            GameObject ob = obj;
            //ob.transform.parent = _blocks[index];
            ob.transform.position = _blocks[index].position + new Vector3(0, 0.5f, 0);
            Debug.Log($"block : {_blocks[index].position}, obj : {ob.transform.position}");
            if(index >= _blocks.Length/2)           // ������ ������ �����Ǹ� ���� �̵�, ���� ������ �����Ǹ� ���� �̵����� ����
            {
                ob.GetComponent<BBFriendStates>().SetLeftMoving(false);
            }
            else
            {
                ob.GetComponent<BBFriendStates>().SetLeftMoving(true);
            }
        }
        else
        {
            Debug.LogError($"failed : {type.ToString()} / obj : {obj}");
        }

        _isCreating = false;

        yield return null;
    }
}
