using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    public float sprintMultiplier = 1.1f;

    public Rigidbody2D rb;
    public float jumpForce = 15f;
    public bool isGrounded;
    
    void Start()
    {
        
    }

    void Update()
    {
        Movement();

        Sprint();

        Jump();
    }

    void Movement()
    {
        Vector2 direction = new Vector2(1, 0) * Input.GetAxisRaw("Horizontal");
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Sprint()
    {
        Vector2 direction = new Vector2(1, 0) * Input.GetAxisRaw("Horizontal");

        if (direction.x != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * (speed * sprintMultiplier) * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }

    public IEnumerator SpeedPowerUp(float speedPowerTime)
    {
        float timer = 0;
        while (timer < speedPowerTime)
        {
            speed = 14f;
            timer += Time.deltaTime;
            yield return null;
        }
        speed = 7f;
    }

    public IEnumerator JumpPowerUp(float jumpPowerTime)
    {
        float timer = 0;
        while (timer < jumpPowerTime)
        {
            jumpForce = 30f;
            timer += Time.deltaTime;
            yield return null;
        }
        jumpForce = 15f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("Speed Power"))
        {
            StartCoroutine(SpeedPowerUp(2f));
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Jump Power"))
        {
            StartCoroutine(JumpPowerUp(2f));
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
