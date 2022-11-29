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
    [SerializeField] private int initialPositionX = -9;
    [SerializeField] private int initialPositionY = -4;

    int currentSceneId = SceneManager.GetActiveScene().buildIndex;


    private bool doubleJump;

    private Rigidbody2D player;
    // Awake is called before Start
    private void Awake()

    {
        player = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player.transform.localPosition = new Vector2(initialPositionX, initialPositionY);

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
    }
    private void Jump()
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

                doubleJump = !doubleJump;
            }
        }
        if (Input.GetButtonUp("Jump") && player.velocity.y > 0f)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
        }
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Vector3.right * Time.deltaTime * speed;
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
            SceneManager.LoadScene(currentSceneId);
        }
    }

}
