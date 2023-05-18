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
        if(collision.CompareTag("Line") && _playerControl.GetGrounded() == true)    // �÷��̾ ���� ���� �� ����
        {

            StartCoroutine(ResetBlocks(collision));



            //if (_count > 16)
            //{
            //    _count = 0;
            //}

            //_count++;
            //if(_count <= 16)
            //{
            //    Debug.Log("�ڸ� �ٲ� Ƚ���� " + _count);
            //    collision.transform.position = new Vector3(collision.transform.position.x, _blocks[_blockIndex].position.y - 2f, 0);
            //    //Debug.Log("Changed position is " + collision.transform.position);
            //    collision.GetComponent<SpriteRenderer>().enabled = true;
            //    collision.GetComponent<Collider2D>().isTrigger = false;
            //}
            //else
            //{
            //    if (_parent == collision.transform._parent)          // �� ������ �θ� ������Ʈ�� ������ �����̳� ģ�� ���� ��󿡼� ����
            //    {
            //        return;
            //    }
            //    _parent = collision.transform._parent;
            //    _parent.GetComponent<BBCreateItem>().CreateStar();
            //    _parent.GetComponent<BBCreateFriend>().CreateFriend();

            //    _blockIndex++;                             // ������ �� ���ġ�� �߻��� �� ������ _blocks �迭�� �ε����� +1�Ͽ� �����ϰ�
            //    _blockIndex = (_blockIndex % _length);     // �迭�� ���̷� ���� �������� ���� �� ���ġ���� ������ �ε����� ������ (5 -> 0 -> 1 -> 2 -> 3 -> 4 -> 5 ...)

            //    _count = 0;
            //}

            // ���� �� ���ġ �ڵ�
            //collision.transform.position = new Vector3(collision.transform.position.x, _lineResetter.position.y, 0);
            //collision.GetComponent<SpriteRenderer>().enabled = true;
            //collision.GetComponent<Collider2D>().isTrigger = false;
            //if (_parent == collision.transform._parent)
            //{
            //    return;
            //}
            //_parent = collision.transform._parent;
            //_parent.GetComponent<BBCreateItem>().CreateStar();
            //_parent.GetComponent<BBCreateFriend>().CreateFriend();
        }
    }

    IEnumerator ResetBlocks(Collider2D collision)
    {
        if(collision.transform.parent == _parent)
        {
            yield break;
        }

         _parent = collision.transform.parent;
        Transform[] children = _parent.GetComponentsInChildren<Transform>(true);           // ��Ȱ��ȭ�� ���� ã��
        List<Transform> tempList = new List<Transform>();
        foreach (var child in children)
        {
            if (child == _parent)
            {
                child.transform.position = new Vector3(child.transform.position.x, _blocks[_blockIndex].position.y - 2f, 0);
                continue;
            }
            else if (!child.CompareTag("Line"))
            {
                continue;
            }
            child.gameObject.SetActive(true);             // ��Ȱ��ȭ�� ���� Ȱ��ȭ
            tempList.Add(child);
        }

        foreach (var block in tempList)
        {
            block.transform.position = new Vector3(block.transform.position.x, _blocks[_blockIndex].position.y - 2f, 0);
            //Debug.Log("Changed position is " + collision.transform.position);
            block.GetComponent<SpriteRenderer>().enabled = true;
            block.GetComponent<Collider2D>().isTrigger = false;
        }

        _parent.GetComponent<BBCreateItem>().CreateStar();
        _parent.GetComponent<BBCreateFriend>().CreateFriend();

        Debug.Log($"_parent position :{_parent.position}\t_blocks[{_blockIndex}] position : {_blocks[_blockIndex].position}");
        _blockIndex++;                             // ������ �� ���ġ�� �߻��� �� ������ _blocks �迭�� �ε����� +1�Ͽ� �����ϰ�
        _blockIndex = (_blockIndex % _length);     // �迭�� ���̷� ���� �������� ���� �� ���ġ���� ������ �ε����� ������ (5 -> 0 -> 1 -> 2 -> 3 -> 4 -> 5 ...)

        yield return null;
    }
}
