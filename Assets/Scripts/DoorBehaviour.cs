using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    Vector2 initPos;
    Vector2 newPos;
    void Start()
    {
        initPos = transform.position;
        newPos = initPos + new Vector2(0, 2.6f);
    }

    void Update()
    {
        if (GetComponent<UniversalActivator>().activated)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, Time.smoothDeltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initPos, Time.smoothDeltaTime);
        }
    }
}