using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    GameObject Player;
    Animator animator;
    [SerializeField]
    UniversalActivator activator;

    void Start()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange()){
            activator.activated = !activator.activated;
            animator.SetBool("Active", activator.activated);
        }
        if (animator.GetBool("Active") != activator.activated)
            animator.SetBool("Active", activator.activated);
    }

    bool isInRange()
    {
        return Vector2.Distance(transform.position, Player.transform.position) <= 0.2f;
    }
}
