using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    public bool pressed = false;
    [SerializeField] Sprite[] sprites;
    public UniversalActivator activator;
    void OnTriggerStay2D(Collider2D collider)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        if(collider.CompareTag("Player") || collider.CompareTag("Pushable"))
        {
            pressed = true;
            activator.activated = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        if(collider.CompareTag("Player") || collider.CompareTag("Pushable"))
        {
            pressed = false;
            activator.activated = false;
        }
    }
}
