using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperTrigger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float _offset;

    void Update()
    {
        float playerPos = player.transform.position.y;
        transform.position = new Vector3(transform.position.x, playerPos + _offset, transform.position.z);
    }
}
