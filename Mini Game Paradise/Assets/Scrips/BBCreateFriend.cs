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

        // 친구 생성 확률 66%로 설정
        float chance = Random.Range(0f, 1f);
        if (chance > 0.66f)
        {
            _isCreating = false;
            yield break;
        }

        // 생성 확률 내에 들어오면 친구 생성
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
            if(index >= _blocks.Length/2)           // 오른쪽 블럭에서 생성되면 우측 이동, 왼쪽 블럭에서 생성되면 좌측 이동으로 설정
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
