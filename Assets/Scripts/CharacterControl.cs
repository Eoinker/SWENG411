using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float maxMoveSpeed;

    [Header("Jump")]
    public float jumpStrength;
    public bool isGrounded; // Tracks if the player is on the ground

    private const float MOVE_ADJUSTEMENT = 1000f;
    private int moveDirection;
    private Rigidbody2D rb;

    public LayerMask groundLayer; // Specify what counts as ground

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (moveDirection == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        rb.AddForce(Vector2.right * moveSpeed * MOVE_ADJUSTEMENT * Time.deltaTime * moveDirection, ForceMode2D.Force);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
    }

    public void SetMoveDirection(int direction)
    {
        moveDirection = direction;
    }

    public void TryJump()
    {
        
        if (isGrounded) // Only allow jumping when grounded
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }

    #region Collision Handling

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
            Debug.Log("Player is grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player stops colliding with ground
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
            Debug.Log("Player is not grounded");
        }
    }

    #endregion

}
