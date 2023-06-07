using System.Numerics;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float crouchScale = 0.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isCrouching = false;
    public bool isGrounded = false;
    private int jumpCount = 0;
    public int maxJumpCount = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Verificar se o jogador está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Movimentação horizontal
        float moveInput = Input.GetAxis("Horizontal");

        if (!isCrouching)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput * speed * crouchScale, rb.velocity.y);
        }

        // Virar o personagem na direção correta
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Pular
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                jumpCount = 1;
            }
            else if (!isGrounded && jumpCount < maxJumpCount)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                jumpCount++;
            }
        }

        // Agachar
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
                isCrouching = true;
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
                isCrouching = false;
            }
        }
    }
}