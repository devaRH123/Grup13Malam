using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    float distance;
    [SerializeField]
    float deltaMult;
    bool isPlayerMoving;
    Vector2 initPos;
    Vector2 newPos;
    Vector2 playerOffset;
    List<GameObject> targets = new();
    List<Vector2> objectOffsets = new();
    GameObject Player;
    void Start()
    {
        initPos = transform.position;
        newPos = initPos + direction * distance;
    }

    void Update()
    {
        if (GetComponent<UniversalActivator>().activated)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, deltaMult * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initPos, deltaMult * Time.fixedDeltaTime);
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

    void OnTriggerStay2D(Collider2D other)
    {        
        if (other.tag != "Player" && !targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
            objectOffsets.Add(other.gameObject.transform.position - transform.position);
        }
        if (other.tag == "Player")
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
        if(other.tag == "Player")
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
