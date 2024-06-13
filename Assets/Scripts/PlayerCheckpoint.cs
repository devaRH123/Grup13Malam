using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().lastCheckpoint = transform;
        }
    }
}
