using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private static ItemPool instance;
    public static ItemPool Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<ItemPool>();

                if(instance == null)
                {
                    Debug.LogError("Instance doesn't exist.");
                    return null;
                }
            }
            return instance;
        }
    }

    Dictionary<string, List<GameObject>> _itemPool = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> _ItemPool
    {
        get
        {
            return _itemPool;
        }
    }

    [SerializeField] GameObject _yellowSingleStar;
    [SerializeField] GameObject _greenSingleStar;
    [SerializeField] GameObject _orangeSingleStar;

    [SerializeField] GameObject _yellowDoubleStar;
    [SerializeField] GameObject _greenDoubleStar;
    [SerializeField] GameObject _orangeDoubleStar;

    [SerializeField] GameObject _yellowTripleStar;
    [SerializeField] GameObject _greenTripleStar;
    [SerializeField] GameObject _orangeTripleStar;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this.GetComponent<ItemPool>())
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CreateDictionary();
    }

    // 각 풀에 게임오브젝트 넣는 함수
    void AddPool(string key)
    {
        switch(key)
        {
            case "YELLOW_SINGLE":
                GameObject ySingleStar = Instantiate(_yellowSingleStar, transform);
                ySingleStar.SetActive(false);
                _itemPool[key].Add(ySingleStar);
                break;
            case "GREEN_SINGLE":
                GameObject gSingleStar = Instantiate(_greenSingleStar, transform);
                gSingleStar.SetActive(false);
                _itemPool[key].Add(gSingleStar);
                break;
            case "ORANGE_SINGLE":
                GameObject oSingleStar = Instantiate(_orangeSingleStar, transform);
                oSingleStar.SetActive(false);
                _itemPool[key].Add(oSingleStar);
                break;

            case "YELLOW_DOUBLE":
                GameObject yDoubleStar = Instantiate(_yellowDoubleStar, transform);
                yDoubleStar.SetActive(false);
                _itemPool[key].Add(yDoubleStar);
                break;
            case "GREEN_DOUBLE":
                GameObject gDoubleStar = Instantiate(_greenDoubleStar, transform);
                gDoubleStar.SetActive(false);
                _itemPool[key].Add(gDoubleStar);
                break;
            case "ORANGE_DOUBLE":
                GameObject oDoubleStar = Instantiate(_orangeDoubleStar, transform) as GameObject;
                oDoubleStar.SetActive(false);
                _itemPool[key].Add(_orangeDoubleStar);
                break;

            case "YELLOW_TRIPLE":
                GameObject yTripleStar = Instantiate(_yellowTripleStar, transform);
                yTripleStar.SetActive(false);
                _itemPool[key].Add(yTripleStar);
                break;
            case "GREEN_TRIPLE":
                GameObject gTripleStar = Instantiate(_greenTripleStar, transform);
                gTripleStar.SetActive(false);
                _itemPool[key].Add(gTripleStar);
                break;
            case "ORANGE_TRIPLE":
                GameObject oTripleStar = Instantiate(_orangeTripleStar, transform);
                oTripleStar.SetActive(false);
                _itemPool[key].Add(oTripleStar);
                break;
        }
    }

    void CreateDictionary()
    {
        _itemPool["YELLOW_SINGLE"] = new List<GameObject>();
        _itemPool["GREEN_SINGLE"] = new List<GameObject>();
        _itemPool["ORANGE_SINGLE"] = new List<GameObject>();

        _itemPool["YELLOW_DOUBLE"] = new List<GameObject>();
        _itemPool["GREEN_DOUBLE"] = new List<GameObject>();
        _itemPool["ORANGE_DOUBLE"] = new List<GameObject>();

        _itemPool["YELLOW_TRIPLE"] = new List<GameObject>();
        _itemPool["GREEN_TRIPLE"] = new List<GameObject>();
        _itemPool["ORANGE_TRIPLE"] = new List<GameObject>();
    }

    // 풀에서 스타 아이템을 빼는 함수
    public GameObject PoolOut(string key)
    {
        // 딕셔너리가 생성되었지만 들어간 내용이 없는 경우
        if(_itemPool.Count == 0)
        {
            CreateDictionary();
        }

        // 딕셔너리가 있지만 키 값이 잘못된 경우
        if(_itemPool.ContainsKey(key) == false)
        {
            Debug.LogError("Invalid Key");
            return null;
        }

        // 딕셔너리가 있고 키 값도 유효하지만 리스트가 안 만들어진 경우
        _itemPool.TryGetValue(key, out var list);
        if (list == null)
        {
            Debug.LogError("Invalid List");
            return null;
        }

        if (list.Count == 0)
        {
            AddPool(key);
        }

        int lastIndex = list.Count - 1;
        GameObject star = list[lastIndex];
        list.Remove(star);
        star.SetActive(true);

        return star;
    }

    // 제거된 아이템을 회수하는 함수
    public void PoolIn(GameObject star, _eItemType type)
    {
        switch (type)
        {
            case _eItemType.YELLOW_SINGLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["YELLOW_SINGLE"].Add(star);
                break;
            case _eItemType.GREEN_SINGLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["GREEN_SINGLE"].Add(star);
                break;
            case _eItemType.ORANGE_SINGLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["ORANGE_SINGLE"].Add(star);
                break;

            case _eItemType.YELLOW_DOUBLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["YELLOW_DOUBLE"].Add(star);
                break;
            case _eItemType.GREEN_DOUBLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["GREEN_DOUBLE"].Add(star);
                break;
            case _eItemType.ORANGE_DOUBLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["ORANGE_DOUBLE"].Add(star);
                break;

            case _eItemType.YELLOW_TRIPLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["YELLOW_TRIPLE"].Add(star);
                break;
            case _eItemType.GREEN_TRIPLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["GREEN_TRIPLE"].Add(star);
                break;
            case _eItemType.ORANGE_TRIPLE:
                star.transform.SetParent(transform);
                star.SetActive(false);
                _itemPool["ORANGE_TRIPLE"].Add(star);
                break;
        }
    }
}
