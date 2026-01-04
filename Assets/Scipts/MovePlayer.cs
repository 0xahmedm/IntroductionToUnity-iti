using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkRadius = 0.1f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private float moveX;

    public GameObject inventoryPanel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        Jump();
        HandleAnimation();

        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveX, 0f, 0f);
        transform.position += move * speed * Time.fixedDeltaTime;
    }
    void HandleInput()
    {
        moveX = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    void HandleAnimation()
    {
        anim.SetBool("isRunning", moveX != 0);
        anim.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    public void Die()
    {
        Debug.Log("Player Died");
        gameObject.SetActive(false);
    }
}
