using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;    

public class playerBehaviour : MonoBehaviour

{
    [SerializeField] private float speed = 4;
    [SerializeField] public float jumpPower = 5f;
    [SerializeField] public float radius = .4f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float initialPositionX = -8.02f;
    [SerializeField] private float initialPositionY = -4.15f;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dieSound;
    [SerializeField] private AudioSource winSound;
    Animator playerAnimator;
    SpriteRenderer playerRenderer;


    public bool isDoubleJumpEnabled = true;

    int sceneNumber;
    private bool doubleJump;
    private Rigidbody2D player;

    // Awake is called before Start
    private void Awake()

    {
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (sceneNumber == 1)
        {
            isDoubleJumpEnabled = false;
        }else
        {
            isDoubleJumpEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }
    private void Jump()
    {

        if (isDoubleJumpEnabled)
        {
            if (onLand() && !Input.GetButton("Jump"))
            {
                doubleJump = false;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (onLand() || doubleJump)
                {
                    // DOuble jump here
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                    jumpSound.Play();

                    doubleJump = !doubleJump;
                }
            }
            if (Input.GetButtonUp("Jump") && player.velocity.y > 0f)
            {
                player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
            }
        } else
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (onLand())
                {
                    // DOuble jump here
                    player.velocity = new Vector2(player.velocity.x, jumpPower);
                    jumpSound.Play();
                }
            }
            if (Input.GetButtonUp("Jump") && player.velocity.y > 0f)
            {
                player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
            }
        }
       
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("isRunning", true);
            transform.position += Vector3.right * Time.deltaTime * speed;
            if (playerRenderer.flipX == true)
            {
                playerRenderer.flipX = false;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerAnimator.SetBool("isRunning", true);
            transform.position -= Vector3.right * Time.deltaTime * speed;
            if (playerRenderer.flipX == false)
            {
                playerRenderer.flipX = true;
            }
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, radius);
    }
    private bool onLand()
    {
        
        return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tueur"))
        {
            // faire delay here
            Die();
        }
        if (other.CompareTag("DoubleJump"))
        {
            Destroy(other.gameObject);
            isDoubleJumpEnabled = true;
        }
        if (other.CompareTag("Porte"))
        {
            winSound.Play();
            passLevel();
        }
    }
    private void Die()
    {
        dieSound.Play();
        player.transform.localPosition = new Vector2(initialPositionX, initialPositionY);

        //SceneManager.LoadScene(sceneNumber);
    }
    private void passLevel()
    {
        SceneManager.LoadScene(sceneNumber + 1);
    }


}
