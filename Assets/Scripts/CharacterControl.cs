using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public CharacterCommandManager commandManager; // Assign this in the Inspector


    [Header("Movement")]
    public float moveSpeed;
    public float maxMoveSpeed;

    [Header("Jump")]
    public float jumpStrength;
    public bool isGrounded; // Tracks if the player is on the ground
    public LayerMask groundLayer; // Specify what counts as ground

    //
    public static Action OnHazardHit;


    private const float MOVE_ADJUSTEMENT = 1000f;
    private int moveDirection;
    private Rigidbody2D rb;
    public Vector2 respawnPoint;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateMovement();
        UpdateGrounded();
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

    private void UpdateGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        if (hit)
        {
            isGrounded = true;
        } else {
            isGrounded =  false;
        }
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
        if (collision.gameObject.CompareTag("Hazard"))
        {
            // Call hazard event
            OnHazardHit?.Invoke();
        }
    }

    #endregion

    public void Respawn()
    {
        // Move the character to the respawn point
        transform.position = respawnPoint;
        // Fix character rotation
        transform.rotation = Quaternion.identity;
        // Reset physics (velocity, rotation, etc.)
        rb.velocity = Vector2.zero;


        moveDirection = 0;
    }

}
