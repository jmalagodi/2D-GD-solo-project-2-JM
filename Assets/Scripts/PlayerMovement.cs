using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 12f;

    Rigidbody2D rb;
    bool grounded;

    public SpriteRenderer sr;

    public Sprite idleSprite;
    public Sprite moveSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // FLIP SPRITE
        if (move > 0)
            sr.flipX = false;
        else if (move < 0)
            sr.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        if (Mathf.Abs(move) > 0.2f)
        {
            sr.sprite = moveSprite;
        }
        else
        {
            sr.sprite = idleSprite;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            grounded = false;
    }
}