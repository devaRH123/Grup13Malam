
using UnityEngine;
using static LeanTween;

public class PushableBehaviour : MonoBehaviour
{
    PlayerControl Player;
    Transform PlayerPos;
    Rigidbody2D rb;
    Vector2 initPos;
    public bool onGround;
    public bool isGrabbed;
    [SerializeField] float followDistance;
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();
        initPos = transform.position;
    }

    void Update() {
        if(isGrabbed) Grabbed(); 
    }

    void Grabbed()
    {
        PlayerPos = Player.transform;
        Vector2 direction = Player.isFacingRight ? Vector2.right : Vector2.left;
        int absDirectionX = Mathf.Abs((int)direction.x);

        Vector2 targetPosition = Vector2.zero;

        switch (absDirectionX)
        {
            case 0: // Kiri ke kiri
                targetPosition = PlayerPos.position;
                break;
            case 1: // Kiri ke kanan, Kanan ke kanan, Kanan ke kiri
                targetPosition = direction.x > 0 ?
                    (Vector2)PlayerPos.position + (Vector2)PlayerPos.right * followDistance * PlayerPos.localScale.x :
                    (Vector2)PlayerPos.position - (Vector2)PlayerPos.right * followDistance * PlayerPos.localScale.x;
                break;
        }
        move(gameObject, targetPosition, 0f).setEase(LeanTweenType.easeOutSine);
    }

    public void Reset()
    {
        transform.position = initPos;
        rb.velocity = Vector2.zero;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("ground")|| other.CompareTag("Pushable"))
        {
            onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("ground") || other.CompareTag("Pushable"))
        {
            if (isGrabbed) isGrabbed = false;
            onGround = false;
        }
    }
}
