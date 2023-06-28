using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        float playerPos = player.transform.position.y;
        transform.position = new Vector3(transform.position.x, playerPos, transform.position.z);
    }
}
