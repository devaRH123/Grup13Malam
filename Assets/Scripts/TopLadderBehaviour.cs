using UnityEngine;

public class TopLadderBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (playerTransform.position.y < transform.position.y)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.position.y > transform.position.y)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if (other.transform.position.y < transform.position.y)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
