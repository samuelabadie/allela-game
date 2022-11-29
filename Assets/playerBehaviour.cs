using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour

{
    [SerializeField] private float speed = 5;
    [SerializeField] public float jumpPower = 5f;
    [SerializeField] public float radius = .5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
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


}
