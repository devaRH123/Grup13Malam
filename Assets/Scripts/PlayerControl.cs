using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D playerRB;
    SpriteRenderer sprite;
    Animator animator;
    float hAxis;
    Vector2 direction;
    public float speed;
    [SerializeField] float jumpPower;
    [SerializeField] bool onGround;
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;
    [SerializeField] float gizDistance;
    Vector2 gizPosition;
    [SerializeField] LayerMask boxMask;
    GameObject pushable;
    public bool isFacingRight;
    [SerializeField] PushableBehaviour pushBeh;
    public Transform lastCheckpoint;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        pushable = GameObject.FindWithTag("Pushable");
        pushBeh = pushable.GetComponent<PushableBehaviour>();
        // audioSource = GetComponent<AudioSource>();
        isFacingRight = true;
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
        if (Input.GetKeyUp(KeyCode.E)) UnPush();
        if (!onGround || (onGround && pushBeh.isGrabbed && !pushBeh.onGround)) UnPush();        
    }

    public void Movement()
    {
        hAxis = Input.GetAxis("Horizontal");
        direction = new (hAxis, 0);
        transform.Translate(speed * Time.deltaTime * direction);
        Facing();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRB.velocity = new Vector2(0, 1) * jumpPower;
            animator.Play("Base Layer.Player_JumpOff", 0, 0);
            // audioSource.clip = audioClips[1];
            // audioSource.Play();
        }
    }

    void Facing()
    {
        if (hAxis > 0 && !isFacingRight && !pushBeh.isGrabbed)
        {      
            sprite.flipX = false; 
            isFacingRight = true;
        }
        else if (hAxis < 0 && isFacingRight && !pushBeh.isGrabbed)
        {
            sprite.flipX = true;
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
            animator.Play( isFacingRight ? "Base Layer.Player_Push" : "Base Layer.Player_Pull", 0, 0);
            animator.SetBool("isPushPull", true);
            pushable = ray.collider.gameObject;
            pushable.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            pushable.GetComponent<FixedJoint2D>().enabled = true;
            pushBeh = pushable.GetComponent<PushableBehaviour>();
            pushBeh.isGrabbed = true;
        }
    }

    void UnPush()
    {
        pushable.GetComponent<FixedJoint2D>().connectedBody = null;
        pushable.GetComponent<FixedJoint2D>().connectedAnchor = Vector2.zero;
        pushable.GetComponent<FixedJoint2D>().enabled = false;
        pushBeh.isGrabbed = false;
        animator.SetBool("isPushPull", false);
        if(Input.GetKeyUp(KeyCode.E))
        {
            if(!isFacingRight && !sprite.flipX) sprite.flipX = true; 
            animator.Play( hAxis != 0 ? "Base Layer.Player_Idle" : "Base Layer.Player_Walk", 0, 0);
        }
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
            animator.speed = 1;
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
