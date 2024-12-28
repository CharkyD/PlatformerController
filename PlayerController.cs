using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveDir;
    public bool isRight;
    private void Update()
    {
        rb.velocity = new Vector2(moveDir.x * speed, rb.velocity.y);
        if(isRight && moveDir.x > 0)
        {
            Flip();
        }else if(!isRight && moveDir.x < 0)
        {
            Flip();
        }
    }
    public void Movement(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(IsGrounded() && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, 16f);
        }
        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, groundLayer);
    }
    private void Flip()
    {
        isRight = !isRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f; // localSclae = localSclae.x * -1;
        transform.localScale = localScale;
    }
}
