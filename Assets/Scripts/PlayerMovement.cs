using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseClimbingSpeed = 5f;
    [SerializeField] float baseJumpSpped = 20f;
    [SerializeField] float baseMovementSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;
    Rigidbody2D playerRigidBody;
    float baseGravityScale;

    public bool isAlive{ get; private set; }

    bool isClimbing
    {
        get
        {
            return Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        }
    }

    bool isRunning
    {
        get
        {
            return Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        }
    }

    bool isTouchingEnemy
    {
        get
        {
            return playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"));
        }
    }

    bool isTouchingGround
    {
        get
        {
            return playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        }
    }

    bool isTouchingClimable
    {
        get
        {
            return playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        }
    }

    void Start()
    {
        isAlive = true;

        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        baseGravityScale = playerRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }

        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if(value.isPressed && isTouchingGround)
        {
            playerRigidBody.velocity += new Vector2(0f, baseJumpSpped);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
    }

    void ClimbLadder()
    {
        if (!isTouchingClimable)
        {
            playerRigidBody.gravityScale = baseGravityScale;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 playerVelocity = new Vector2(playerRigidBody.velocity.x, (moveInput.y * baseClimbingSpeed));
        playerRigidBody.velocity = playerVelocity;
        playerRigidBody.gravityScale = 0;

        playerAnimator.SetBool("isClimbing", isClimbing);
    }

    void Die()
    {
        if(isTouchingEnemy)
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRigidBody.velocity = deathKick;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void FlipSprite()
    {
        if (isRunning)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2((moveInput.x * baseMovementSpeed), playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        playerAnimator.SetBool("isRunning", isRunning);
    }
}
