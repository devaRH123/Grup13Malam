using UnityEngine;

public class TrampolineBehaviour : MonoBehaviour
{
    public Vector2 velo;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().velocity = velo;
        animator.SetBool("JumpedOn", true);
    }
    void OnTriggerExit2D (Collider2D other)
    {
        animator.SetBool("JumpedOn", false);
    }
}
