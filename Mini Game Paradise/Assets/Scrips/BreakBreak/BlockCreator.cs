using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] GameObject[] _blockPrefabs;
    public int _blockCount;

    public void CreateBlock(Vector2 blockPosition)
    {
        // 번갈아가면서 서로 색깔이 다른 블럭을 만들 때 사용
        int nextBlockColor = _blockCount % _blockPrefabs.Length;
        GameObject go = Instantiate(_blockPrefabs[nextBlockColor]);
        go.transform.position = blockPosition;
        _blockCount++;
    }
}
