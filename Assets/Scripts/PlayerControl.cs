using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D playerRB;
    SpriteRenderer spriteRenderer;
    float hAxis;
    Vector2 direction;
    public float speed;
    [SerializeField] float jumpPower;
    [SerializeField] bool onGround;
    Animator animator;
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    [SerializeField] float gizDistance;
    Vector2 gizPosition;
    [SerializeField] LayerMask boxMask;
    GameObject pushable;
    public bool isFacingRight;
    public PushableBehaviour pushBeh;
    public Transform lastCheckpoint;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
        direction = Vector2.right;
        isFacingRight = true;
        pushable = GameObject.FindWithTag("Pushable");
        pushBeh = pushable.GetComponent<PushableBehaviour>();
        transform.position = lastCheckpoint.position;
    }

    void Update()
    {
        Animations();
        Jump();
        Movement();
        gizPosition = new(transform.position.x ,transform.position.y + 0.34f);
        if (Input.GetKeyDown(KeyCode.E)) PushBox();
    }
    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.E) || !onGround || (onGround && pushBeh.isGrabbed && !pushBeh.onGround)) UnPush();        
    }

    public void Movement()
    {
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, 0);
        transform.Translate(speed * Time.deltaTime * direction);
        Facing();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            animator.Play("Base Layer.Player_JumpOff", 0, 0);
            playerRB.velocity = new Vector2(0, 1) * jumpPower;
            // audioSource.clip = audioClips[1];
            // audioSource.Play();
        }
    }

    void Facing()
    {
        if (hAxis > 0 && !isFacingRight && pushBeh.isGrabbed == false)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
        else if (hAxis < 0 && isFacingRight && pushBeh.isGrabbed == false)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
    }

    void Animations()
    {
        animator.SetFloat("Moving", Mathf.Abs(hAxis));
        animator.SetBool("onGround", onGround);
    }

    void PushBox()
    {
        direction = isFacingRight ? Vector2.right : Vector2.left;
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D ray = Physics2D.Raycast(gizPosition, direction * transform.localScale.x, gizDistance, boxMask);
        if (ray.collider != null && ray.collider.gameObject.CompareTag("Pushable") && onGround)
        {
            pushable = ray.collider.gameObject;
            pushBeh = pushable.GetComponent<PushableBehaviour>();
            pushable.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            pushable.GetComponent<FixedJoint2D>().enabled = true;
            pushBeh.isGrabbed = true;
        }
    }

    void UnPush()
    {
        pushable.GetComponent<FixedJoint2D>().connectedBody = null;
        pushable.GetComponent<FixedJoint2D>().connectedAnchor = Vector2.zero;
        pushable.GetComponent<FixedJoint2D>().enabled = false;
        pushBeh.isGrabbed = false;
    }

    public void Death()
    {
        transform.position = lastCheckpoint.position;
        if (pushBeh.isGrabbed) UnPush();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ground") || other.CompareTag("Pushable") || other.CompareTag("Bouncer") || other.CompareTag("MovingPlatform"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ground") || other.CompareTag("Pushable") || other.CompareTag("Bouncer") || other.CompareTag("MovingPlatform"))
        {
            onGround = false;
        }
    }
}
