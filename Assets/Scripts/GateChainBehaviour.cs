using UnityEngine;

public class GateChainBehaviour : MonoBehaviour
{
    Vector2 initPos;
    Vector2 newPos;
    bool IsInRange = false;
    [SerializeField] UniversalActivator activator;

    void Start()
    {
        initPos = transform.position;
        newPos = initPos + Vector2.down;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsInRange){
            activator.activated = !activator.activated;
            Vector2 targetPos = activator.activated ? newPos : initPos;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);
        }
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
