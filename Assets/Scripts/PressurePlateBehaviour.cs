using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    public bool pressed = false;
    public bool boxPressed = false;
   
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" || collider.tag == "Box")
        {
            if (collider.tag == "Player")
            {
                pressed = true;
            }
            if (collider.tag == "Box")
            {
                boxPressed = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player" || collider.tag == "Box")
        {
            if (collider.tag == "Player")
            {
                pressed = false;
                
            }
            else if (collider.tag == "Box")
            {
                boxPressed = false;
            }
        }
    }
}
