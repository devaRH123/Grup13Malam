using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float vertical;
    public float speed = 8f;
    public bool isLadder;
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

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Ladder"))
        {
            if (isClimbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            rb.gravityScale = 1f;
            rb.velocity = rb.velocity.normalized;
            isLadder = false;
            isClimbing = false;
        }
    }
}