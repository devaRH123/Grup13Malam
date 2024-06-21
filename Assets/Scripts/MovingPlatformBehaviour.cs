using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    public Vector2 direction;
    public float distance;
    [SerializeField] float deltaMult;
    AudioSource audioSource;
    bool isMoving = false;
    bool isPlayerMoving;
    Vector2 initPos;
    Vector2 newPos;
    Vector2 playerOffset;
    List<GameObject> targets = new();
    List<Vector2> objectOffsets = new();
    GameObject Player;
    UniversalActivator activator;
    void Start()
    {
        activator = GetComponent<UniversalActivator>();
        audioSource = GetComponent<AudioSource>();
        initPos = transform.position;
        newPos = initPos + direction * distance;
    }

    void Update()
    {
        Vector2 targetPos = activator.activated ? newPos : initPos;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, deltaMult * Time.deltaTime);
        StartCoroutine(CheckMoving());
        if(!isMoving)
            audioSource.Play();
        
        if (Player != null && direction.x != 0)
        {
            Player.GetComponent<PlayerControl>().speed = (Vector2)transform.position == targetPos ? 2 : 4.5f;
        }

        isPlayerMoving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
 
        // Safely handle potential null targets and stale entries
        if (targets.Count != objectOffsets.Count)
        {
            Debug.LogError("Targets and objectOffsets lists have different lengths. Please ensure they are kept in sync.");
            return; // Prevent further processing if lists are out of sync
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                Debug.LogError("Found a null target at index " + i + ". Removing it.");
                targets.RemoveAt(i);
                objectOffsets.RemoveAt(i);
                continue;
            }
            if(targets[i].gameObject.GetComponent<PushableBehaviour>().isGrabbed){
                targets.RemoveAt(i);
                objectOffsets.RemoveAt(i);
            }else{
                targets[i].transform.position = (Vector2)transform.position + objectOffsets[i];
            }
        }
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

    void OnTriggerStay2D(Collider2D other)
    {        
        if (!other.CompareTag("Player") && !targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
            objectOffsets.Add(other.gameObject.transform.position - transform.position);
        }
        if (other.CompareTag("Player"))
        {
            if (isPlayerMoving)
            {
                Player = null;
                playerOffset = Vector2.zero;
            }
            else
            {
                Player = other.gameObject;
                playerOffset = other.gameObject.transform.position - transform.position;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (targets.Contains(other.gameObject))
        {
            int index = targets.IndexOf(other.gameObject);
            targets.RemoveAt(index);
            objectOffsets.RemoveAt(index);
        }
        if(other.CompareTag("Player"))
        {
            Player = null;
        }
    }
    void LateUpdate(){
        if (Player != null) {
            Player.transform.position = (Vector2)transform.position + playerOffset;
        }
    }
}
