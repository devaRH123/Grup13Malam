using UnityEngine;
using System.Collections.Generic;

public class HazardsBehaviour : MonoBehaviour
{
    public List<GameObject> targets = new();
    void Start()
    {
        targets.AddRange(GameObject.FindGameObjectsWithTag("Pushable"));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().Death();
            Reset();
        }
    }

    void Reset()
    {
        foreach (GameObject target in targets)
        {
            target.GetComponent<PushableBehaviour>().Reset();
        }
    }
}
