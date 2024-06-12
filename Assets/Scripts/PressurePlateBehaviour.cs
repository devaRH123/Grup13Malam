using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    public bool pressed = false;
    [SerializeField]
    Sprite inactive;    
    [SerializeField]
    Sprite active;
    public UniversalActivator activator;
    void OnTriggerStay2D(Collider2D collider)
    {
        GetComponent<SpriteRenderer>().sprite = active;
        if(collider.tag == "Player" || collider.tag == "Pushable")
        {
            pressed = true;
            activator.activated = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GetComponent<SpriteRenderer>().sprite = inactive;
        if(collider.tag == "Player" || collider.tag == "Pushable")
        {
            pressed = false;
            activator.activated = false;
        }
    }
}
