using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] GameObject[] _blockPrefabs;

    int _blockCount;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBlock(Vector2 blockPosition)
    {
        // 번갈아가면서 서로 색깔이 다른 블럭을 만들 때 사용
        int nextBlockColor = _blockCount % _blockPrefabs.Length;
        GameObject go = Instantiate(_blockPrefabs[nextBlockColor]);
        go.transform.position = blockPosition;
        _blockCount++;
    }
}
