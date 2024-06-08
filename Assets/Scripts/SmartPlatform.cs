using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPlatform : MonoBehaviour
{

    public GameObject Player;

    BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // if player y is below a platform turn off the coll
        if(Player.transform.position.y < transform.position.y)
        {
            coll.enabled = false;
        }

        // if player y is above a platform turn on the coll
        if(Player.transform.position.y > transform.position.y)
        {
            coll.enabled = true;
        }

        // if user pushdown then turn off the coll
        if(Input.GetAxis("Vertical") < 0)
        {
            coll.enabled = false;
        }
    }
}
