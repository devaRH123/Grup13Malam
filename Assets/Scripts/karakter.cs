using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    float hAxis;
    Vector2 direction;

    [SerializeField] float speed = 3f;
    [SerializeField] float jumpPower = 5f;

    Rigidbody2D rb;

    [SerializeField] bool onGround = false;

    Animator animator; // Corrected capitalization to "Animator"

    [SerializeField]
    AudioClip[] audioClips;
    AudioSource audioSource;

    [SerializeField]
    Transform BG;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        Jump();
        Facing();
        Animations(); // Added semicolon here
    }

    // Update is called once per frame
    void Movement()
    {
        // Monitor horizontal keypresses and apply movement to player object
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, 0);

        transform.Translate(direction * Time.deltaTime * speed);
    }

    void Jump()
    {
        // If spacebar pressed then apply velocity to rb on y-axis
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            rb.velocity = new Vector2(0, 1) * jumpPower;
            
            audioSource.clip = audioClips[1];
            audioSource.Play(); // Changed AudioSource to audioSource
        }
    }

    void Facing()
    {
        // If player is moving left scale = -1
        if (hAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            BG.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        // If player is moving right scale = 1
        else if (hAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            BG.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
    }

    void Animations()
    {
        // If player is moving then play running animation
        animator.SetFloat("Speed", Mathf.Abs(hAxis));
        animator.SetBool("OnGround", onGround); // Corrected method name to SetBool
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If trigger enters object with tag "ground" then onGround = true
        if (col.CompareTag("ground"))
        {
            onGround = true;
        }

        if (col.CompareTag("enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (col.tag == "collectible")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play(); // Changed AudioSource to audioSource
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // If trigger exits object with tag "ground" then onGround = false
        if (col.CompareTag("ground"))
        {
            onGround = false;
        }
    }
}
