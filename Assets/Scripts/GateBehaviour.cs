using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    Collider2D coll;
    Animator animator;
    bool activator;
    void Start()
    {
        coll = transform.GetChild(0).gameObject.transform.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        activator = GetComponent<UniversalActivator>().activated;
        if (activator)
        {
            coll.enabled = false;
            animator.SetBool("Active", activator);
        }
        else
        {
            coll.enabled = true;
            animator.SetBool("Active", activator);
        }
    }
}
