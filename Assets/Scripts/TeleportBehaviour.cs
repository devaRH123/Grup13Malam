using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject portal;
    GameObject player;
    bool isPlayerInTrigger = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInTrigger = false;
        }
    }

    void TeleportPlayer()
    {
        player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y - 1);
    }
}

