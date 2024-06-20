using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    Animator animator;
    bool IsInRange = false;
    [SerializeField] UniversalActivator activator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsInRange){
            activator.activated = !activator.activated;
            animator.SetBool("Active", activator.activated);
        }
        if (animator.GetBool("Active") != activator.activated)
            animator.SetBool("Active", activator.activated);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            IsInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            IsInRange = false;
    }
}
