using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isTouchingWall;
    private bool rightWall;
    private bool startGame = true;

    public float jumpForce = 5f;
    public float runSpeed = 6f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (startGame || !rightWall)
        {
            rb.AddForce(new Vector2(10f, jumpForce), ForceMode2D.Impulse);
            rightWall = true;
            startGame = false;
        }
        else if (rightWall)
        {
            rb.AddForce(new Vector2(-10f, jumpForce), ForceMode2D.Impulse);
            rightWall = !rightWall;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            ActivateGravity(false);
            Run();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
            ActivateGravity(true);
        }
    }

    private void ActivateGravity(bool activate)
    {
        rb.gravityScale = activate ? 1f : 0f;
    }

    void Run()
    {
        rb.velocity = new Vector2(rb.velocity.x, runSpeed * transform.localScale.y);
    }
}

