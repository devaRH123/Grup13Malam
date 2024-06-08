using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject portal;
    private GameObject player;
    private bool isPlayerInTrigger = false;

    void Start()
    {
        player = GameObject.FindWithTag("karakter");
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "karakter")
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "karakter")
        {
            isPlayerInTrigger = false;
        }
    }

    private void TeleportPlayer()
    {
        player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
    }
}

