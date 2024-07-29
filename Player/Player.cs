using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeedH = 3f;
    public float moveSpeedV = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public int MaxMP = 100;

    private Rigidbody rb;
    private int jumpsRemaining;
    private Transform player;
    private Animator animator;
    private bool isGrounded;

    public CharacterLoader characterLoader;
    public void Start()
    {
        characterLoader = FindObjectOfType<CharacterLoader>();
        if (characterLoader != null)
        {
            characterLoader.LoadCustomisation(gameObject);
        }
        else
        {
            Debug.LogError("CharacterLoader не найден!");
        }

        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
        player = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        OverJump();
    }
    void Move()
    {
        float vInput = Input.GetAxis("Vertical") * moveSpeedV;
        float hInput = Input.GetAxis("Horizontal") * moveSpeedH;

        Vector3 moveDirection = (transform.forward * vInput + transform.right * hInput) * Time.fixedDeltaTime;
        rb.MovePosition(this.transform.position + moveDirection);

        if (isGrounded && (hInput > 0))
        {
            animator.SetTrigger("Right");
        }
        else if (isGrounded && (hInput < 0))
        {
            animator.SetTrigger("Left");
        }
        else if (isGrounded && (vInput > 0))
        {
            animator.SetTrigger("Go");
        }
        else if (isGrounded && (vInput < 0))
        {
            animator.SetTrigger("Back");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
    /*
    void MoveWithJoistick()
    {
        {
            float horizontalInput = movingJoystick.Horizontal;
            float verticalInput = movingJoystick.Vertical;

            if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
            {
                Vector2 normalizedInput = new Vector2(horizontalInput, verticalInput).normalized;
                horizontalInput = normalizedInput.x;
                verticalInput = normalizedInput.y;
            }

            Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput)
                * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDirection);

            if (grounded && (horizontalInput != 0f || verticalInput != 0f))
            {
                animator.SetTrigger("Go");
            }
            else if (grounded)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
    */
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpsRemaining--;
            isGrounded = false;
            animator.SetTrigger("Jump");           
        }
    }
    void OverJump()
    {
        float vInput = Input.GetAxis("Vertical");

        if (vInput > 0)
        {
            if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
            {
                isGrounded = false;
                animator.SetTrigger("OverJump");
            }
        }
        else if (vInput < 0)
        {
            if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
            {
                isGrounded = false;
                animator.SetTrigger("BackJump");
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (rb.velocity != null)
            {
                animator.SetTrigger("Go");
            }
            jumpsRemaining = maxJumps;
        }
    }
}
