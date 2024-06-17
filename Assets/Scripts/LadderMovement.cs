using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    public bool onLadder;
    bool onGround;
    Animator animator;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update ()
    {
        onGround = animator.GetBool("onGround");
        if(onLadder && !onGround)
        {
            animator.Play(  "Base Layer.Player_Climb", 0, 0);   
        }
        if(onLadder && onGround)
        {
            animator.Play(  "Base Layer.Player_Idle", 0, 0);   
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Ladder"))
        {
            float vAxis = Input.GetAxis("Vertical");
            Vector2 direction = new(0, vAxis);
            onLadder = true;
            if(vAxis != 0)
            { 
                animator.SetBool("onLadder", onLadder);
                animator.speed = 1;
                transform.Translate(speed * Time.deltaTime * direction);

                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
            else if (vAxis == 0)
            {
                animator.speed = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            onLadder = false;        
            rb.gravityScale = 1f;
            animator.SetBool("onLadder", onLadder = false);
        }
    }
}