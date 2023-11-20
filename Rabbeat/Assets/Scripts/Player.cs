using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditorInternal;
using UnityEngine;



public class Player : MonoBehaviour
{
    public AudioSource soundJump;
    public AudioSource soundRun;

    [SerializeField] private Transform playerPosition;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForceY = 10f;
    [SerializeField] private float jumpForceX = 10f;
    [SerializeField] private float runSpeed = 6f;

    private bool firstPress = true;
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

    public CurrencyManager cm;
    private int carrotsCount;

    private void Start()
    {
        previousPosition = playerPosition.position;
        EnablePlayerMovement();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Run(isRunUp);
    }

    #region Player Movement
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(firstPress)
            {
                soundRun.mute = false;
                firstPress = false;
            }
             

            if(isTouchingWall || isRun)
                soundJump.Play();

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
            else if (isRun)
            {
                if (rb.velocity.x > 0)
                    rb.AddForce(new Vector2(jumpForceX, 5f), ForceMode2D.Impulse);
                if (rb.velocity.x < 0)
                    rb.AddForce(new Vector2(-jumpForceX, 5f), ForceMode2D.Impulse);
            }
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
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // vertical or horizontal camera movement 
        if (collision.gameObject.CompareTag("GameController"))
        {
            cameraMoveX = !cameraMoveX;
        }
        if (collision.gameObject.CompareTag("Carrot"))
        {
            Destroy(collision.gameObject);
            cm.carrotCount++;
        }
    }

    #region Contact with wall/ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        soundRun.Play();
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
            isRunUp = true;   
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
    #endregion

    #region DeathScreen
    private void DisablePlayerMovement()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void EnablePlayerMovement()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnEnable()
    {
        DamageCollision.OnPlayerDeath += DisablePlayerMovement;
    }

    private void OnDisable()
    {
        DamageCollision.OnPlayerDeath -= DisablePlayerMovement;
    }
    #endregion
}

