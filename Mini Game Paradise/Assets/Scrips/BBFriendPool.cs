using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBFriendPool : MonoBehaviour
{
    Dictionary<string, List<GameObject>> _bbFriendPool = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> _BBFriendPool
    {
        get
        {
            return _bbFriendPool;
        }
    }

    [SerializeField] GameObject _maskDude;
    [SerializeField] GameObject _ninjaFrog;
    [SerializeField] GameObject _pinkMan;
    [SerializeField] GameObject _virtualGuy;

    void Start()
    {
        CreateDictionary();
    }

    void CreateDictionary()
    {
        _bbFriendPool["MASKDUDE"] = new List<GameObject>();
        _bbFriendPool["NINJAFROG"] = new List<GameObject>();
        _bbFriendPool["PINKMAN"] = new List<GameObject>();
        _bbFriendPool["VIRTUALGUY"] = new List<GameObject>();
    }

    // 각 풀에 게임오브젝트 넣는 함수
    void AddPool(string key)
    {
        switch (key)
        {
            case "MASKDUDE":
                GameObject ySingleStar = Instantiate(_maskDude, transform);
                ySingleStar.SetActive(false);
                _bbFriendPool[key].Add(ySingleStar);
                break;
            case "NINJAFROG":
                GameObject gSingleStar = Instantiate(_ninjaFrog, transform);
                gSingleStar.SetActive(false);
                _bbFriendPool[key].Add(gSingleStar);
                break;
            case "PINKMAN":
                GameObject oSingleStar = Instantiate(_pinkMan, transform);
                oSingleStar.SetActive(false);
                _bbFriendPool[key].Add(oSingleStar);
                break;
            case "VIRTUALGUY":
                GameObject yDoubleStar = Instantiate(_virtualGuy, transform);
                yDoubleStar.SetActive(false);
                _bbFriendPool[key].Add(yDoubleStar);
                break;
        }
    }

    public GameObject PoolOut(string key)
    {
        // 딕셔너리가 생성되었지만 들어간 내용이 없는 경우
        if (_bbFriendPool.Count == 0)
        {
            CreateDictionary();
        }

        // 딕셔너리가 있지만 키 값이 잘못된 경우
        if (_bbFriendPool.ContainsKey(key) == false)
        {
            Debug.LogError("Invalid Key");
            return null;
        }

        // 딕셔너리가 있고 키 값도 유효하지만 리스트가 안 만들어진 경우
        _bbFriendPool.TryGetValue(key, out var list);
        if (list == null)
        {
            Debug.LogError("Invalid List");
            return null;
        }

        if (list.Count == 0)
        {
            AddPool(key);
        }

        // 리스트 마지막 인덱스에 위치한 친구 프리팹을 풀에서 꺼냄
        int lastIndex = list.Count - 1;
        GameObject friend = list[lastIndex];
        list.Remove(friend);
        friend.SetActive(true);

        return friend;
    }

    // 제거된 아이템을 회수하는 함수
    public void PoolIn(GameObject friend, _eFriendType type)
    {
        switch (type)
        {
            case _eFriendType.MASKDUDE:
                friend.transform.SetParent(transform);
                friend.SetActive(false);
                _bbFriendPool["MASKDUDE"].Add(friend);
                break;
            case _eFriendType.NINJAFROG:
                friend.transform.SetParent(transform);
                friend.SetActive(false);
                _bbFriendPool["NINJAFROG"].Add(friend);
                break;
            case _eFriendType.PINKMAN:
                friend.transform.SetParent(transform);
                friend.SetActive(false);
                _bbFriendPool["PINKMAN"].Add(friend);
                break;
            case _eFriendType.VIRTUALGUY:
                friend.transform.SetParent(transform);
                friend.SetActive(false);
                _bbFriendPool["VIRTUALGUY"].Add(friend);
                break;
        }
    }
}
