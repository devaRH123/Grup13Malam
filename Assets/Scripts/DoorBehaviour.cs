using System.Collections;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    AudioSource audioSource;
    UniversalActivator activator;
    bool isMoving = false;
    Vector2 initPos;
    Vector2 newPos;
    void Start()
    {
        activator = GetComponent<UniversalActivator>();
        audioSource = GetComponent<AudioSource>();
        initPos = transform.position;
        newPos = initPos + new Vector2(0, 2.6f);
    }

    void Update()
    {
        Vector2 targetPos = activator.activated ? newPos : initPos;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.smoothDeltaTime);
        StartCoroutine(CheckMoving());
        if(!isMoving)
            audioSource.Play();
    }

    IEnumerator CheckMoving()
    {
        Vector2 a = transform.position; 
        yield return new WaitForSeconds(0.1f);
        Vector2 b = transform.position; 

        if (a != b)
            isMoving = true;
        else
            isMoving = false;
    }
}