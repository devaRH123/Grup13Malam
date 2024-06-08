using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float vertical;
    public float speed = 8f;
    public bool isClimbing;
    
    Rigidbody2D rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Climbing();
    }

    void Climbing()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (vertical > 0f)
        {
            isClimbing = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if ((other.CompareTag("Ladder") || other.CompareTag("TopLadder")) && isClimbing == true)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }  
        if ((other.CompareTag("Ladder") || other.CompareTag("TopLadder")) && transform.position.y > other.transform.position.y && Input.GetAxisRaw("Vertical") < 0){
            other.isTrigger = true;
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder") || other.CompareTag("TopLadder"))
        {
            isClimbing = false;        
            rb.gravityScale = 1f;
        }
    }
}