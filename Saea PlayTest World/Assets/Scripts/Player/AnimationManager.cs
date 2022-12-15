using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator animator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 groundCheckSize;
    private bool isGrounded = false;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector3 wallCheckSize;
    private bool isWallDetected = false;

    [SerializeField] private Transform headBonkCheck;
    private bool headBonk = false;

    [SerializeField] private Transform wallCheckTop;
    [SerializeField] private Transform wallCheckBottom;
    [SerializeField] private Vector3 extraWallCheckSize;
    private bool isWallDetectedTop;
    private bool isWallDetectedBottom;

    private bool isWallDetectedTotal;

    private float verticalVelocity;
    private float horizontalVelocity;

    private bool isMovingVertical;
    private bool isMovingHorizontal;

    [SerializeField] private LayerMask whatIsGround;

    private bool canWallSlide;
    private bool canAirDash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("Moving", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetValues();
        if (isMovingHorizontal == false && isMovingVertical == false)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }
        if (isWallDetectedTotal == true && isGrounded == false && canWallSlide == true && isMovingHorizontal == true && verticalVelocity <= 0)
        {
            animator.SetBool("WallSlide", true);
        }
        else
        {
            animator.SetBool("WallSlide", false);
        }
    }

    private void GetValues()
    {
        horizontalVelocity = Input.GetAxisRaw("Horizontal");
        verticalVelocity = rb.velocity.y;
        //Debug.Log(horizontalVelocity);
        if (horizontalVelocity > 0.1 || horizontalVelocity < -0.1)
        {
            isMovingHorizontal = true;
        }
        else
        {
            isMovingHorizontal = false;
        }
        if (verticalVelocity != 0)
        {
            isMovingVertical = true;
        }
        else
        {
            isMovingVertical = false;
        }
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, whatIsGround);
        isWallDetected = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0f, whatIsGround);
        headBonk = Physics2D.OverlapBox(headBonkCheck.position, groundCheckSize, 0f, whatIsGround);
        isWallDetectedTop = Physics2D.OverlapBox(wallCheckTop.position, extraWallCheckSize, 0f, whatIsGround);
        isWallDetectedBottom = Physics2D.OverlapBox(wallCheckBottom.position, extraWallCheckSize, 0f, whatIsGround);
        if (isWallDetected == true && isWallDetectedTop == true && isWallDetectedBottom == true)
        {
            isWallDetectedTotal = true;
        }
        else
        {
            isWallDetectedTotal = false;
        }
    }

    private void UnlockWallSlide()
    {
        canWallSlide = true;
    }
}
