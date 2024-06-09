using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    //public Joystick movingJoystick;
    public int MaxMP = 100;

    private Rigidbody rb;
    private int jumpsRemaining;
    private Transform player;
    private Animator animator;
    private bool grounded;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
        player = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        //StartCoroutine(LoadPlayerPosition());
    }

    void Update()
    {
        Move();
        Jump();
        /*
         if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
         {
             MoveWithJoistick();
         }
         else
         {
             Move();
             Jump();
         }
     }
        */
    }
    void Move()
    {
        float vInput = Input.GetAxis("Vertical") * moveSpeed;
        float hInput = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 moveDirection = (transform.forward * vInput + transform.right * hInput) * Time.fixedDeltaTime;
        rb.MovePosition(this.transform.position + moveDirection);

        if (grounded && (vInput != 0 || hInput != 0))
        {
            animator.SetTrigger("Go");
        }
        else if (grounded)
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
            grounded = false;
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            //SavePlayerPosition();
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
            //SavePlayerPosition();
        }
    }

    private void SavePlayerPosition()
    {
        Vector3 position = player.transform.position;
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.Save();
    }
    private IEnumerator LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");
        player.transform.position = new Vector3(x, y, z);

        yield return null;

    }

}
