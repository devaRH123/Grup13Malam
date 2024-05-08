using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player is below the platform's y position, turn off collider
        if (player.transform.position.y < transform.position.y)
        {
            collider.enabled = false;
        }
        
        //If player y is above platform y turn on collider
        if (player.transform.position.y > transform.position.y)
        {
            collider.enabled = true;
        }

        //If user pushes down then turn off collider
        if (Input.GetAxis("Vertical") < 0)
        {
            collider.enabled = false;
        }
    }
}
