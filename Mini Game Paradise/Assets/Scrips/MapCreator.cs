using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    float _blockWidth = 0.5f;
    float _blockHeight = 0.5f;
    int _blocksInScreen = 12;              // 화면 내 블록 수
    float _offset = 2f;                    // 줄 간격
    int _breakCount = 0;                   // 줄 파괴 횟수

    struct FloorBlock
    {
        public bool _isCreated;       // 블록 생성 여부 판단
        public Vector2 _position;     // 블록의 위치
    }

    FloorBlock _leftLastBlock;      // 마지막에 생성한 왼쪽 블록
    FloorBlock _rightLastBlock;     // 마지막에 생성한 오른쪽 블록
    PlayerControl _player;
    BlockCreator _blockCreator;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        _leftLastBlock._isCreated = false;
        _rightLastBlock._isCreated = false;
        _blockCreator = gameObject.GetComponent<BlockCreator>();
    }



    // ToDo : 방향 전환할 때 블럭이 있는데도 한 번 더 블럭을 생성하는 것을 개선 필요, 오른쪽/왼쪽으로 블럭을 만들 때에도 첫 블럭을 leftLastBlock/rightLastBlock으로 지정하여 블럭을 중복 생성하지 않도록 수정
    void Update()
    {
        // 플레이어 기준 오른쪽으로 화면 절반만큼 블럭이 생성되었는지 체크
        float playerRightPositionX = _player.transform.position.x;
        playerRightPositionX += _blockWidth * (float)(_blocksInScreen / 2.0f);

        // 플레이어 기준 왼쪽으로 화면 절반만큼 블럭이 생성되었는지 체크
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

        // 블록이 생성된 적 없는 경우
        if(_leftLastBlock._isCreated == false)
        {
            blockPosition = _player.transform.position;
            blockPosition -= new Vector2(0, 3);
            blockPosition.x += _blockWidth * (float)(_blocksInScreen / 2.0f);
            blockPosition.y -= _offset * _breakCount;
        }

        // 이전에 생성한 블록이 있는 경우
        else
        {
            blockPosition = _leftLastBlock._position;
        }

        // 최초 생성 위치에서 블록 하나만큼 왼쪽으로 이동 후 생성
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

        // 블록이 생성된 적 없는 경우
        if (_rightLastBlock._isCreated == false)
        {
            blockPosition = _player.transform.position;
            blockPosition -= new Vector2(0, 3);
            blockPosition.x -= _blockWidth * (float)(_blocksInScreen / 2.0f);
            blockPosition.y -= _offset * _breakCount;
        }

        // 이전에 생성한 블록이 있는 경우
        else
        {
            blockPosition = _rightLastBlock._position;
        }

        // 최초 생성 위치에서 블록 하나만큼 오른쪽으로 이동 후 생성
        blockPosition.x += _blockWidth;
        _blockCreator.CreateBlock(blockPosition);
        _blockCreator.CreateBlock(blockPosition - new Vector2(0, _offset * (_breakCount + 1)));
        _blockCreator.CreateBlock(blockPosition - new Vector2(0, _offset * (_breakCount + 2)));
        _rightLastBlock._position = blockPosition;
        _rightLastBlock._isCreated = true;
    }
}
