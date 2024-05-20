using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    
    Rigidbody2D playerRB;
    BoxCollider2D boxColl;
    CapsuleCollider2D capsColl;
    SpriteRenderer spriteRenderer;
    float hAxis;
    Vector2 direction;

    [SerializeField]
    float speed = 5;
    public float jumpPower = 8;

    [SerializeField]
    public bool onGround;

    public bool onBox;

    Animator animator;

    [SerializeField]
    AudioClip[] audioClips;
    AudioSource audioSource;

    [SerializeField]
    // Lives livesScript;

    public float gizDistance = 0.5f;
    public LayerMask boxMask;

    GameObject box;

    public bool isFacingRight;

    public BoxBehaviour boxBeh;
   
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
        capsColl = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        direction = Vector2.right;
        isFacingRight = true;
        box = GameObject.FindWithTag("Box");
    }

    void Update()
    {
        if (onGround ==  false || (onGround && boxBeh.isGrabbed == true && boxBeh.onGround == false))
        {   
           UnPushBox();
        }
        Movement();
        if (Input.GetKeyDown(KeyCode.E))
            PushBox();
        if (Input.GetKeyUp(KeyCode.E))
            UnPushBox();
        Jump();
        Animations();
    }

    void Movement()
    {
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, 0);
        transform.Translate(direction * Time.deltaTime * speed);
        Facing();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (onGround == true || onBox == true))
        {
            playerRB.velocity = new Vector2(0, 1) * jumpPower;
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
    }

    void Facing()
    {
        if (hAxis > 0 && !isFacingRight && boxBeh.isGrabbed == false)
        {
            boxColl.offset = new Vector2(-0.03118515f,0);
            boxColl.size = new Vector2(0.8211477f,1);
            
            capsColl.offset = new Vector2(-0.02831477f, -0.4838917f);
            capsColl.size = new Vector2(0.5875146f, 0.1175466f);
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
        else if (hAxis < 0 && isFacingRight && boxBeh.isGrabbed == false)
        {
            boxColl.offset = new Vector2(0.03141856f,0);
            boxColl.size = new Vector2(0.8177637f,1);

            capsColl.offset = new Vector2(0.03599472f, -0.4838917f);
            capsColl.size = new Vector2(0.5691428f, 0.1175466f);
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
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction * transform.localScale.x, gizDistance, boxMask);
        if (ray.collider != null && ray.collider.gameObject.tag == "Box" && onGround)
        {
            box = ray.collider.gameObject;
            boxBeh = box.GetComponent<BoxBehaviour>();
            box.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<BoxBehaviour>().isGrabbed = true;
        }
    }

    void UnPushBox()
    {
        box.GetComponent<FixedJoint2D>().enabled = false;
        box.GetComponent<BoxBehaviour>().isGrabbed = false;
    }

    private void OnDrawGizmos()
    {
        direction = isFacingRight ? Vector2.right : Vector2.left;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * gizDistance);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "ground" || other.tag == "TopLadder")
        {
            onGround = true;
        }

        if (other.tag == "Box" || other.tag == "Bouncer")
        {
            onBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ground" || other.tag == "TopLadder")
        {
            onGround = false;
        }
        if (other.tag == "Box" || other.tag == "Bouncer")
        {
            onBox = false;            
        }
    }
}
