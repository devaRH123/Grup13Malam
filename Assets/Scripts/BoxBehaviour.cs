using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LeanTween;

public class BoxBehaviour : MonoBehaviour
{
    public PlayerControl pC;

    Rigidbody2D rb;
    public bool onGround;

    public bool isGrabbed = false;
    
    float followDistance = 0.89f;

    public float gizDistance = 0.55f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(isGrabbed) {
            Grabbed();
        }
    }

    void Grabbed()
    {
        Vector2 direction = pC.isFacingRight ? Vector2.right : Vector2.left;
        int absDirectionX = Mathf.Abs((int)direction.x);

        Vector2 targetPosition = Vector2.zero;

        switch (absDirectionX)
        {
            case 0: // Kiri ke kiri
                targetPosition = pC.transform.position;
                break;
            case 1: // Kiri ke kanan, Kanan ke kanan, Kanan ke kiri
                targetPosition = direction.x > 0 ?
                    (Vector2)pC.transform.position + (Vector2)pC.transform.transform.right * followDistance * pC.transform.localScale.x :
                    (Vector2)pC.transform.position - (Vector2)pC.transform.transform.right * followDistance * pC.transform.localScale.x;
                break;
        }
        move(gameObject, targetPosition, 0f).setEase(LeanTweenType.easeOutSine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ground" && onGround == false)
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "ground" && onGround == false && isGrabbed)
        {
            onGround = false;
            isGrabbed = false;
        }
    }
}
