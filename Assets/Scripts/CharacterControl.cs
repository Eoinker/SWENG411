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
    public bool isGrounded;

    private bool isExecuting;
    private const float MOVE_ADJUSTEMENT = 1000f;
    private int moveDirection;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        BeginExecution();
    }

    public void Update()
    {
        UpdateMovement();
    }

    #region Movement

    private void UpdateMovement()
    {
        if (moveDirection == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        } else {
            rb.AddForce(Vector2.right * moveSpeed * MOVE_ADJUSTEMENT * Time.deltaTime * moveDirection, ForceMode2D.Force);
        }

        rb.velocity = new Vector2 (Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
    }

    public void SetMoveDirection(int direction)
    {
        moveDirection = direction;
    }

    #endregion

    #region Jumping

    public void TryJump()
    {
        // This function needs checking system for if the player is grounded or not
        Jump();
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }

    #endregion

    #region Execution

    public bool IsExecuting()
    {
        return isExecuting;
    }

    public void BeginExecution()
    {
        isExecuting = true;
    }

    private void ResumeExecution()
    {
        isExecuting = true;
    }

    private void PauseExecution()
    {
        isExecuting = false;
    }   

    // Pause Execution and then resume after a delay amount in seconds
    public IEnumerator PauseExecutionForSeconds(float delay)
    {
        PauseExecution();
        yield return new WaitForSeconds(delay);
        ResumeExecution();
    }

    // Pause Execution and then resume after a delay amount in frames
    public IEnumerator PauseExecutionForFrames(int frames)
    {
        PauseExecution();
        yield return frames;
        ResumeExecution();
    }

    #endregion
}
