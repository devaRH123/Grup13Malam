using UnityEngine;
using static LeanTween;

public class PushableBehaviour : MonoBehaviour
{
    PlayerControl pC;
    public bool onGround;
    public bool isGrabbed;
    [SerializeField]
    float followDistance;

    void Start()
    {
        pC = GameObject.Find("Player").GetComponent<PlayerControl>();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ground" && onGround == false)
        {
            onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "ground" && onGround == false && isGrabbed)
        {
            onGround = false;
            isGrabbed = false;
        }
    }
}
