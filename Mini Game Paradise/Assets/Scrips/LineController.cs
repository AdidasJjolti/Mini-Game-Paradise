using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] Transform _lineResetter;
    [SerializeField] PlayerControl _playerControl;
    Transform _parent;
    [SerializeField] Transform[] _blocks;
    int _blockIndex;
    int _length;
    int _count;

    void Awake()
    {
        _count = 0;
        _blockIndex = _blocks.Length - 1;
        _length = _blocks.Length;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Line") && _playerControl.GetGrounded() == true)    // 플레이어가 땅에 닿을 때 실행
        {
            Debug.Log("줄이 바뀌지롱, count는 " + _count);

            if(_count > 16)
            {
                _count = 0;
            }

            _count++;
            if(_count <= 16)
            {
                Debug.Log("자리 바뀐 횟수는 " + _count);
                collision.transform.position = new Vector3(collision.transform.position.x, _blocks[_blockIndex].position.y - 2f, 0);
                //Debug.Log("Changed position is " + collision.transform.position);
                collision.GetComponent<SpriteRenderer>().enabled = true;
                collision.GetComponent<Collider2D>().isTrigger = false;
            }
            else
            {
                if (_parent == collision.transform.parent)          // 블럭 묶음인 부모 오브젝트는 아이템 생성이나 친구 생성 대상에서 제외
                {
                    return;
                }
                _parent = collision.transform.parent;
                _parent.GetComponent<BBCreateItem>().CreateStar();
                _parent.GetComponent<BBCreateFriend>().CreateFriend();

                _blockIndex++;                             // 다음에 블럭 재배치가 발생할 때 참조할 _blocks 배열의 인덱스를 +1하여 변경하고
                _blockIndex = (_blockIndex % _length);     // 배열의 길이로 나눈 나머지가 다음 블럭 재배치에서 참조할 인덱스로 설정됨 (5 -> 0 -> 1 -> 2 -> 3 -> 4 -> 5 ...)

                _count = 0;
            }

            // 이전 블럭 재배치 코드
            //collision.transform.position = new Vector3(collision.transform.position.x, _lineResetter.position.y, 0);
            //collision.GetComponent<SpriteRenderer>().enabled = true;
            //collision.GetComponent<Collider2D>().isTrigger = false;
            //if (_parent == collision.transform.parent)
            //{
            //    return;
            //}
            //_parent = collision.transform.parent;
            //_parent.GetComponent<BBCreateItem>().CreateStar();
            //_parent.GetComponent<BBCreateFriend>().CreateFriend();
        }
    }
}
