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

    public Queue<CharacterCommand> commands;

    private const float MOVE_ADJUSTEMENT = 1000f;
    private int moveDirection;
    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetMoveDirection(1);
        InvokeRepeating("toggleMove", 2f, 2f);
        InvokeRepeating("Jump", 1f, 2f);
    }

    public void Update()
    {
        UpdateMovement();
    }
    
    public void toggleMove()
    {
        SetMoveDirection(-moveDirection);
        rb.velocity = new Vector2(0, rb.velocity.y);
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private void UpdateMovement()
    {
        if (moveDirection == 0)
        {
            return;
        } else {
            rb.AddForce(Vector2.right * moveSpeed * MOVE_ADJUSTEMENT * Time.deltaTime * moveDirection, ForceMode2D.Force);
        }

        rb.velocity = new Vector2 (Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
    }

    private void LateUpdate()
    {
        
    }

    public void SetMoveDirection(int direction)
    {
        moveDirection = direction;
    }

    
    // public void Move(int direction)
    // {
    //     rb.velocity = new Vector2(10 * direction, rb.velocity.y);
    // }       

    // public void MoveLeft()
    // {
    //     Move(-1);
    // }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
    }
}
