using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        float playerPos = player.transform.position.y;
        transform.position = new Vector3(transform.position.x, playerPos, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TurnTurnGameManager GameManager = FindObjectOfType<TurnTurnGameManager>();
            GameManager.SetGameOver();
        }
    }
}
