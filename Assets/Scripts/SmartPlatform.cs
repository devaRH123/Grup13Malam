using UnityEngine;

public class SmartPlatform : MonoBehaviour
{

    public GameObject Player;

    BoxCollider2D coll;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if(Player.transform.position.y < transform.position.y)
        {
            coll.enabled = false;
        }
        if(Player.transform.position.y > transform.position.y)
        {
            coll.enabled = true;
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            coll.enabled = false;
        }
    }
}
