using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal;
using UnityEngine;



public class Player : MonoBehaviour
{
    public AudioSource music;
    public AudioSource soundJump;
    public AudioSource soundRun;

    [SerializeField] private Transform playerPosition;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForceY = 10f;
    [SerializeField] private float jumpForceX = 10f;
    [SerializeField] private float runSpeed = 6f;

    private bool isTouchingWall;
    private bool rightWall;
    private bool startGame = true;
    private bool isRunUp, isRun;
    private Vector3 previousPosition;
    private bool cameraMoveX;
    public bool CameraMoveX
    {
        get { return cameraMoveX; }
        private set { CameraMoveX = cameraMoveX; }
    }

    private void Start()
    {
        previousPosition = playerPosition.position;
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Run(isRunUp);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            soundJump.Play();

            isRunUp = false; isRun = false;

            if (startGame)
                music.Play();

            if (startGame || (!rightWall && isTouchingWall))
            {
                rightWall = true;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(jumpForceX, jumpForceY), ForceMode2D.Impulse);
                startGame = false;
            }
            else if (rightWall && isTouchingWall)
            {
                rightWall = !rightWall;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(-jumpForceX, jumpForceY), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameController"))
        {
            cameraMoveX = !cameraMoveX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            isRunUp = true;
            soundRun.Play();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingWall = false;
            isRun = true;
            isRunUp = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
            isRunUp = false;
            soundRun.Stop();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isRun = false;
        }
    }

    void Run(bool isRunUp)
    {
        Vector3 currentPosition = playerPosition.position;
        float deltaX = currentPosition.x - previousPosition.x;
        if (isRunUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, runSpeed);
        }
        else if (!isRunUp) 
        {
            if (deltaX > 0)
            {
                rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            }
            else if (deltaX < 0)
            {
                rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            }
            previousPosition = currentPosition;
        }
    }
}

