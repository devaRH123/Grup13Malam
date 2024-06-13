using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
