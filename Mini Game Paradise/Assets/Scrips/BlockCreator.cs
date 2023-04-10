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
        // �����ư��鼭 ���� ������ �ٸ� ���� ���� �� ���
        int nextBlockColor = _blockCount % _blockPrefabs.Length;
        GameObject go = Instantiate(_blockPrefabs[nextBlockColor]);
        go.transform.position = blockPosition;
        _blockCount++;
    }
}
