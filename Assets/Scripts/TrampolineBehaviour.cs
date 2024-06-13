using System.Collections;
using UnityEngine;

public class TrampolineBehaviour : MonoBehaviour
{
    public Vector2 velo;
    Animator animator;
    bool jumpedOn;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("JumpedOn", jumpedOn);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (!other.CompareTag("Hazard"))
        {
            jumpedOn = true;
            other.GetComponent<Rigidbody2D>().velocity = velo;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        jumpedOn = false;
    }
}
