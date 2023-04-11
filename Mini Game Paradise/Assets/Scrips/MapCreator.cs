using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    float _blockWidth = 0.5f;
    float _blockHeight = 0.5f;
    int _blocksInScreen = 12;              // ȭ�� �� ��� ��
    float _offset = 2f;                    // �� ����
    int _breakCount = 0;                   // �� �ı� Ƚ��

    struct FloorBlock
    {
        public bool _isCreated;       // ��� ���� ���� �Ǵ�
        public Vector2 _position;     // ����� ��ġ
    }

    FloorBlock _leftLastBlock;      // �������� ������ ���� ���
    FloorBlock _rightLastBlock;     // �������� ������ ������ ���
    PlayerControl _player;
    BlockCreator _blockCreator;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        _leftLastBlock._isCreated = false;
        _rightLastBlock._isCreated = false;
        _blockCreator = gameObject.GetComponent<BlockCreator>();
    }



    // ToDo : ���� ��ȯ�� �� ���� �ִµ��� �� �� �� ���� �����ϴ� ���� ���� �ʿ�, ������/�������� ���� ���� ������ ù ���� leftLastBlock/rightLastBlock���� �����Ͽ� ���� �ߺ� �������� �ʵ��� ����
    void Update()
    {
        // �÷��̾� ���� ���������� ȭ�� ���ݸ�ŭ ���� �����Ǿ����� üũ
        float playerRightPositionX = _player.transform.position.x;
        playerRightPositionX += _blockWidth * (float)(_blocksInScreen / 2.0f);

        // �÷��̾� ���� �������� ȭ�� ���ݸ�ŭ ���� �����Ǿ����� üũ
        float playerLeftPositionX = _player.transform.position.x;
        playerLeftPositionX -= _blockWidth * (float)(_blocksInScreen / 2.0f);

        if (_player.GetLeftMoving() == true)
        {
            while (_leftLastBlock._position.x > playerLeftPositionX)
            {
                CreateLeftBlock();
            }
        }
        else
        {
            while (_rightLastBlock._position.x < playerRightPositionX)
            {
                CreateRightBlock();
            }
        }
    }

    public void CreateLeftBlock()
    {
        Vector2 blockPosition;

        // ����� ������ �� ���� ���
        if(_leftLastBlock._isCreated == false)
        {
            blockPosition = _player.transform.position;
            blockPosition -= new Vector2(0, 3);
            blockPosition.x += _blockWidth * (float)(_blocksInScreen / 2.0f);
            blockPosition.y -= _offset * _breakCount;
        }

        // ������ ������ ����� �ִ� ���
        else
        {
            blockPosition = _leftLastBlock._position;
        }

        // ���� ���� ��ġ���� ��� �ϳ���ŭ �������� �̵� �� ����
        blockPosition.x -= _blockWidth;
        _blockCreator.CreateBlock(blockPosition);
        _blockCreator.CreateBlock(blockPosition - new Vector2(0, _offset * (_breakCount + 1)));
        _blockCreator.CreateBlock(blockPosition - new Vector2(0, _offset * (_breakCount + 2)));
        _leftLastBlock._position = blockPosition;
        _leftLastBlock._isCreated = true;
    }

    public void CreateRightBlock()
    {
        Vector2 blockPosition;

        // ����� ������ �� ���� ���
        if (_rightLastBlock._isCreated == false)
        {
            blockPosition = _player.transform.position;
            blockPosition -= new Vector2(0, 3);
            blockPosition.x -= _blockWidth * (float)(_blocksInScreen / 2.0f);
            blockPosition.y -= _offset * _breakCount;
        }

        // ������ ������ ����� �ִ� ���
        else
        {
            blockPosition = _rightLastBlock._position;
        }

        // ���� ���� ��ġ���� ��� �ϳ���ŭ ���������� �̵� �� ����
        blockPosition.x += _blockWidth;
        _blockCreator.CreateBlock(blockPosition);
        _blockCreator.CreateBlock(blockPosition - new Vector2(0, _offset * (_breakCount + 1)));
        _blockCreator.CreateBlock(blockPosition - new Vector2(0, _offset * (_breakCount + 2)));
        _rightLastBlock._position = blockPosition;
        _rightLastBlock._isCreated = true;
    }
}
