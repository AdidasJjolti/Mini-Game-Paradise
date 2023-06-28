using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        if(transform.position.y - player.transform.position.y > transform.localScale.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 4 * transform.localScale.y);
        }
    }
}
