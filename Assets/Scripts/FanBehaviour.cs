using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    bool activator;
    Animator animator;
    GameObject wind;

    void Start()
    {
        wind = transform.GetChild(0).gameObject;
        animator = transform.GetChild(1).gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        activator = GetComponent<UniversalActivator>().activated;
        if(activator){
            wind.SetActive(true);
            animator.SetBool("Active", activator);
        }
        else
        {
            wind.SetActive(false);
            animator.SetBool("Active", activator);
        }
    }
}
