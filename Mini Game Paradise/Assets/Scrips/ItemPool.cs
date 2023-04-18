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
                instance.GetComponent<ItemPool>();
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
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        _itemPool["yellowSingle"] = new List<GameObject>();
        _itemPool["greenSingle"] = new List<GameObject>();
        _itemPool["orangeSingle"] = new List<GameObject>();

        _itemPool["yellowDouble"] = new List<GameObject>();
        _itemPool["greenDouble"] = new List<GameObject>();
        _itemPool["orangeDouble"] = new List<GameObject>();

        _itemPool["yellowTriple"] = new List<GameObject>();
        _itemPool["greenTriple"] = new List<GameObject>();
        _itemPool["orangeTriple"] = new List<GameObject>();
    }

    // 각 풀에 게임오브젝트 넣는 함수
    void AddPool(string key)
    {
        switch(key)
        {
            case "yellowSingle":
                GameObject ySingleStar = Instantiate(_yellowSingleStar);
                ySingleStar.SetActive(false);
                _itemPool[key].Add(ySingleStar);
                break;
            case "greenSingle":
                GameObject gSingleStar = Instantiate(_greenSingleStar);
                gSingleStar.SetActive(false);
                _itemPool[key].Add(gSingleStar);
                break;
            case "orangeSingle":
                GameObject oSingleStar = Instantiate(_orangeSingleStar);
                oSingleStar.SetActive(false);
                _itemPool[key].Add(oSingleStar);
                break;

            case "yellowDouble":
                GameObject yDoubleStar = Instantiate(_yellowDoubleStar);
                yDoubleStar.SetActive(false);
                _itemPool[key].Add(yDoubleStar);
                break;
            case "greenDouble":
                GameObject gDoubleStar = Instantiate(_greenDoubleStar);
                gDoubleStar.SetActive(false);
                _itemPool[key].Add(gDoubleStar);
                break;
            case "orangeDouble":
                GameObject oDoubleStar = Instantiate(_orangeDoubleStar);
                oDoubleStar.SetActive(false);
                _itemPool[key].Add(_orangeDoubleStar);
                break;

            case "yellowTriple":
                GameObject yTripleStar = Instantiate(_yellowTripleStar);
                yTripleStar.SetActive(false);
                _itemPool[key].Add(yTripleStar);
                break;
            case "greenTriple":
                GameObject gTripleStar = Instantiate(_greenTripleStar);
                gTripleStar.SetActive(false);
                _itemPool[key].Add(gTripleStar);
                break;
            case "orangeTriple":
                GameObject oTripleStar = Instantiate(_orangeTripleStar);
                oTripleStar.SetActive(false);
                _itemPool[key].Add(oTripleStar);
                break;
        }
    }

    // 풀에서 스타 아이템을 빼는 함수
    GameObject PoolOut(string key)
    {
        var items = _itemPool[key];

        if (items.Count == 0)
        {
            AddPool(key);
        }

        int lastIndex = items.Count - 1;
        GameObject star = items[lastIndex];
        items.Remove(star);
        star.SetActive(true);

        return star;
    }

    // 제거된 아이템을 회수하는 함수
    public void PoolIn(GameObject star, _eItemType type)
    {
        switch (type)
        {
            case _eItemType.YELLOW_SINGLE:
                star.SetActive(false);
                _itemPool["yellowSingle"].Add(star);
                break;
            case _eItemType.GREEN_SINGLE:
                star.SetActive(false);
                _itemPool["greenSingle"].Add(star);
                break;
            case _eItemType.ORANGE_SINGLE:
                star.SetActive(false);
                _itemPool["orangeSingle"].Add(star);
                break;

            case _eItemType.YELLOW_DOUBLE:
                star.SetActive(false);
                _itemPool["yellowDouble"].Add(star);
                break;
            case _eItemType.GREEN_DOUBLE:
                star.SetActive(false);
                _itemPool["greenDouble"].Add(star);
                break;
            case _eItemType.ORANGE_DOUBLE:
                star.SetActive(false);
                _itemPool["orangeDouble"].Add(star);
                break;

            case _eItemType.YELLOW_TRIPLE:
                star.SetActive(false);
                _itemPool["yellowTriple"].Add(star);
                break;
            case _eItemType.GREEN_TRIPLE:
                star.SetActive(false);
                _itemPool["greenTriple"].Add(star);
                break;
            case _eItemType.ORANGE_TRIPLE:
                star.SetActive(false);
                _itemPool["orangeTriple"].Add(star);
                break;
        }
    }
}
