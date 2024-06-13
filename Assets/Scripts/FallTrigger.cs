using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = new Vector2(Player.transform.position.x, -9.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player.GetComponent<PlayerControl>().Death();
        }
    }
}
