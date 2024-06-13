using UnityEngine;

public class SmartPlatform : MonoBehaviour
{

    public GameObject Player;
    BoxCollider2D[] coll;
    [SerializeField] bool playerOntrigger;
    void Start()
    {
        coll = GetComponents<BoxCollider2D>();
        // Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (playerOntrigger)
        {
            if (Player.transform.position.y > transform.position.y)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    coll[0].excludeLayers = LayerMask.GetMask("Player"); 
                }
                else if(Input.GetKeyUp(KeyCode.S)) 
                {
                    coll[0].excludeLayers = LayerMask.GetMask("");
                }
            }
            else
            {
                coll[0].excludeLayers = LayerMask.GetMask("Player"); 
            }
        }
        else
        {
            coll[0].excludeLayers = LayerMask.GetMask("");
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerOntrigger = true;
            Player = other.gameObject;            
        }        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerOntrigger = false;
            Player = null;
        }
    }
}
