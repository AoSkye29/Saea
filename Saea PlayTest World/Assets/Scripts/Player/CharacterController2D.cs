using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    private UI UI;

    // booleans for unlocking abilities
    [Header("Unlock abilities")]
    public bool unlockDoubleJump = false; // whether the player has unlocked double jump (mid-air jump)
    public bool unlockAirDash = false; // whether the player has unlocked mid-air dash
    public bool unlockWallSlide = false; // whether the player has unlocked wall slide
    public bool unlockWallJump = false; // whether the player has unlocked wall jump

    // movement values/ power
    [Header("Movement values/ power")]
    [SerializeField] private float speed = 10f; // how fast the character moves
    [SerializeField] private float jumpForce = 10f; // how high the character can jump
    public bool canMove = true; // whether the player currently has control over their characters' movement

    // flip character model (private)
    private float moveInput; // movement input by player
    private bool facingRight = true; // whether the character should be facing right
    private int facingDirection = 1; // what direction the character is facing
    //private bool wallSlideFlip = false;

    // check if character is on the ground or in the air
    [Header("Collision checks")]
    // ground collision
    [SerializeField] private Transform groundCheck; // rectangle to check ground collision
    [SerializeField] private Vector3 groundCheckSize; // size of groundCheck rectangle
    private bool isGrounded = false; // whether the character is on the ground
    // wall collision
    [SerializeField] private Transform wallCheck; // rectangle to check wall collision
    [SerializeField] private Vector3 wallCheckSize; // size of wallCheck rectangle
    private bool isWallDetected = false; // whether the character is moving against a wall
    [SerializeField] private Transform wallCheckTop;
    [SerializeField] private Transform wallCheckBottom;
    [SerializeField] private Vector3 extraWallCheckSize;
    private bool isWallDetectedTop;
    private bool isWallDetectedBottom;
    private bool isWallDetectedTotal;

    // ceiling collision
    [SerializeField] private Transform headBonkCheck;
    private bool headBonk = false;
    
    [SerializeField] private LayerMask whatIsGround; // which layers count as ground

    // (high) jumping
    [Header("How long the player can hold jump")]
    [SerializeField] private float jumpTime = 0.35f; // how long the character can increase the jump height
    private float jumpTimeCounter; // how long the character has been jumping
    private bool isJumping; // whether the character is jumping

    // dashing
    [Header("Values for dashing")]
    [SerializeField] private float dashPower = 20f; // how fast the character can go while dashing
    [SerializeField] private float dashTime = 0.2f; // how long the dash lasts
    private bool canDash = false; // whether the character can currently use the dash ability
    [SerializeField] private float groundDashGravity = 15; // the gravity setting while ground dashing
    [SerializeField] private bool isDashing = false; // whether the character is dashing

    // double jumping
    [Header("Amount of double jumps")]
    [SerializeField] private int maxExtraJumps = 1; // max amount of extra jumps the character has
    private int extraJumps; // how many extra jumps the character currently has

    // wall slide
    [Header("Values for wall slide")]
    [SerializeField] private float wallSlideSpeedMultiplier = 0.1f; // how much wall sliding slows you down
    [SerializeField] private float fastWallSlideSpeedMultiplier = 0.95f; // how much fast wall sliding slows you down
    private bool canWallSlide; // whether the character can use wall side
    private bool isWallSliding; // whether the character is currently using wall slide

    // wall jump
    [Header("Values for wall jump")]
    [SerializeField] private Vector2 wallJumpDirection; // in what direction the force of the wall jump is applied
    private bool canWallJump = true; // whether the character can use wall jump

    // virtual inputs (for controller support) (private)
    private PlayerControls controls; // which control action map to use
    
    // RigidBody
    [Header("Rigidbody gravity")]
    public float gravityScale = 5f; // how strong the gravity affecting the rigidbody is
    private Rigidbody2D rb; // the characters' rigidbody

    // executed once before start
    private void Awake()
    {
        controls = new PlayerControls();
        
        controls.Gameplay.Enable();
    }

    // executed once at start
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // set rb to the characters' rigidbody
        rb.gravityScale = gravityScale; // set rigidbody's gravityScale to the gravityScale variable

        UI = GameObject.Find("InGame").GetComponent<UI>();
    }

    // executed every frame
    private void Update()
    {
        if (canMove == false || isDashing == true)
        {
            return;
        }

        // reset once-per-airborne abilities
        if (isGrounded == true)
        {
            extraJumps = maxExtraJumps;
            jumpTimeCounter = jumpTime;
            isJumping = true;
            canDash = true;
            canWallJump = true;
        }

        InputHandler(); // handles player inputs
    }

    // executed every physics update
    private void FixedUpdate()
    {
        CollisionCheck(); // check collisions (ground and wall)

        if (isGrounded == false && controls.Gameplay.Jump.IsPressed() == false)
        {
            isJumping = false;
        }

        if (headBonk == true && isGrounded == false)
        {
            isJumping = false;
        }

        if (isDashing == true)
        {
            return;
        }

        if (canMove == true)
        {
            // check whether player is inputting movement and move character
            moveInput = Input.GetAxisRaw("Horizontal");
            if (moveInput != 0) gameObject.transform.SetParent(null);
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            // check whether player model is facing right way and fix it
            FixOrientation();
        }
        
    }

    // check collisions (ground and wall)
    private void CollisionCheck()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, whatIsGround); // check for ground collision

        isWallDetected = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0f, whatIsGround); // check for wall collision
        isWallDetectedTop = Physics2D.OverlapBox(wallCheckTop.position, extraWallCheckSize, 0f, whatIsGround);
        isWallDetectedBottom = Physics2D.OverlapBox(wallCheckBottom.position, extraWallCheckSize, 0f, whatIsGround);

        headBonk = Physics2D.OverlapBox(headBonkCheck.position, groundCheckSize, 0f, whatIsGround); // check for ceiling collision

        if (isWallDetected == true && isWallDetectedTop == true && isWallDetectedBottom == true)
        {
            isWallDetectedTotal = true;
        }
        else
        {
            isWallDetectedTotal = false;
        }

        // cancel wall slide if no more wall detected
        if (isWallDetectedTotal == false)
        {
            isWallSliding = false;
        }

        // allow player to start wall sliding once peak of jump is reached
        if (isGrounded == false && rb.velocity.y <= 0)
        {
            canWallSlide = true;
        }

        // give player control over character when grounded or falling
        if (isGrounded == true || rb.velocity.y <= 0)
        {
            canMove = true;
        }
    }

    // handles player inputs
    private void InputHandler()
    {
        if (canMove == true)
        {
            // start wall slide when character is next to wall and not moving upwards
            if (isWallDetectedTotal == true && canWallSlide == true && rb.velocity.y <= 0)
            {
                WallSlide();
            }

            // start jump when player inputs jump button (and ends jump when player releases jump button)
            if (controls.Gameplay.Jump.IsPressed() || controls.Gameplay.Jump.WasReleasedThisFrame())
            {
                Jump();
            }

            // start wall jump when player inputs jump button while facing wall
            if (isWallDetected == true && canWallJump == true)
            {
                isWallSliding = true;
                if (controls.Gameplay.Jump.WasPressedThisFrame())
                {
                    WallJump();
                }
            }

            // start dash when player inputs dash button
            if (controls.Gameplay.Dash.WasPressedThisFrame())
            {
                Dash();
            }

            if (controls.Gameplay.Attack.WasPressedThisFrame())
            {
                transform.GetComponent<PlayerAttack>().Attack();
            }
        }
    }

    // handles wall slide
    private void WallSlide()
    {
        if (unlockWallSlide == true)
        {
            // wall slide only works if player is actively moving toward the wall while being right next to it
            if (facingRight == true && moveInput > 0 || facingRight == false && moveInput < 0)
            {
                isWallSliding = true;
                float multiplier;
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    multiplier = fastWallSlideSpeedMultiplier;
                }
                else
                {
                    multiplier = wallSlideSpeedMultiplier;
                }
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * multiplier); // slows down fall
            }
        }
    }

    // handles wall slide
    private void WallJump()
    {
        if (unlockWallJump == true)
        {
            /*
                applies the same slow effect as the wall slide just before the wall jump starts,
                handles disparity between wall jumping with and without wall sliding unlocked.
                without this extra slow effect,
                the wall jump would get heavily nerfed upon unlocking wall slide
            */
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
            
            isWallSliding = false; // ends wall slide

            canMove = false; // disables player movement

            rb.velocity = new Vector2(0, 0);

            Vector2 direction = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);

            rb.AddForce(direction, ForceMode2D.Impulse); // add force to character to move it at an angle
        }
    }

    // flips character model (by multiplying X scale by -1)
    private void FixOrientation()
    {
        if ((facingRight == false && moveInput > 0 && isWallSliding == false) || (facingRight == true && moveInput < 0 && isWallSliding == false))
        {
            facingRight = !facingRight;
            facingDirection *= -1;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
        /*else if (((facingRight == false && moveInput < 0 && isWallSliding == true && wallSlideFlip == false) || (facingRight == true && moveInput > 0 && isWallSliding == true && wallSlideFlip == false) || (isWallSliding == false && wallSlideFlip == true)) && unlockWallSlide == true && isGrounded == false)
        {
            wallSlideFlip = !wallSlideFlip;
            Vector3 Scaler = transform.Find("PlayerSprite").localScale;
            Scaler.x *= -1;
            transform.Find("PlayerSprite").localScale = Scaler;
        }*/
    }

    // handles jump inputs
    private void Jump()
    {
        if (controls.Gameplay.Jump.WasPressedThisFrame() && isGrounded == true)
        {
            JumpStart(); // starts grounded jump
        }
        else if (controls.Gameplay.Jump.WasPressedThisFrame() && extraJumps > 0 && isWallSliding == false)
        {
            DoubleJump(); // starts double jump
        }
        else if (controls.Gameplay.Jump.WasPressedThisFrame() && extraJumps > 0 && unlockWallJump == false)
        {
            DoubleJump(); // starts double jump
        }
        if (controls.Gameplay.Jump.IsPressed() && isJumping == true) // buttonstate down
        {
            JumpContinue(); // increases jump height while button stays pressed (with time limit)
        }
        if (controls.Gameplay.Jump.WasReleasedThisFrame())
        {
            JumpCancel(); // ends jump (prevents further increasing jump height)
        }
    }

    // starts double jump
    private void DoubleJump()
    {
        if (unlockDoubleJump == true) // checks if double jump has been unlocked
        {
            // start double jump
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            extraJumps--;
        }
    }

    // starts grounded jump
    private void JumpStart()
    {
        rb.velocity = Vector2.up * jumpForce;
        isJumping = true;
        jumpTimeCounter = jumpTime;
    }

    // increases jump height while button stays pressed (with time limit)
    private void JumpContinue()
    {
        if (jumpTimeCounter > 0) // checks if the character can still jump higher
        {
            // makes the character jump higher
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter -= Time.deltaTime;
        }
        else // if player can no longer jump higher
        {
            // ends double jump
            isJumping = false;
        }
    }

    // ends jump (prevents further increasing jump height)
    private void JumpCancel()
    {
        isJumping = false;
    }

    // handles dash inputs
    private void Dash()
    {
        if (controls.Gameplay.Dash.WasPressedThisFrame() && isGrounded == true)
        {
            StartCoroutine(GroundDash()); // grounded dash
        }
        else if (controls.Gameplay.Dash.WasPressedThisFrame() && isGrounded == false && canDash == true)
        {
            StartCoroutine(AirDash()); // mid-air dash
        }
    }

    // grounded dash
    private IEnumerator GroundDash()
    {
        // dashes forward
        canDash = false;
        isDashing = true;
        if (unlockAirDash == false) rb.gravityScale = groundDashGravity; // heightens gravity
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        if (unlockAirDash == false) rb.gravityScale = gravityScale; // reset gravityScale back to original values
        isDashing = false;
    }

    // mid-air dash
    private IEnumerator AirDash()
    {
        if (unlockAirDash == true) // checks if mid-air dash has been unlocked
        {
            // dashes forward with 0 gravity
            canDash = false;
            isDashing = true;
            rb.gravityScale = 0f; // sets gravity to 0 to ensure straight forward dash
            rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
            yield return new WaitForSeconds(dashTime);
            rb.gravityScale = gravityScale; // resets gravityScale back to original values
            isDashing = false;
        }
    }

    // draws gizmos in unity editor to show certain things
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan; // sets gizmo color for all following gizmos

        Gizmos.DrawCube(groundCheck.position, groundCheckSize); // show groundCheck rectangle

        Gizmos.DrawCube(wallCheck.position, wallCheckSize); // show wallCheck rectangle

        Gizmos.DrawCube(headBonkCheck.position, groundCheckSize); // show headBonkCheck rectangle

        Gizmos.color = Color.red; // sets gizmo color for all following gizmos

        Gizmos.DrawCube(wallCheckTop.position, extraWallCheckSize);
        Gizmos.DrawCube(wallCheckBottom.position, extraWallCheckSize);

        Vector2 wallJumpRay = new Vector2(wallJumpDirection.x * 0.03f * -facingDirection, wallJumpDirection.y * 0.03f); // direction of wall jump ray
        Gizmos.DrawRay(transform.position, wallJumpRay); // show wall jump direction
    }

    public void Pickup(int id)
    {
        switch(id)
        {
            case 0:
                unlockAirDash = true;
                UI.EnableAirDash();
                // BroadcastMessage("Winter");
                break;
            case 1:
                unlockDoubleJump = true;
                UI.EnableDoubleJump();
                // BroadcastMessage("Spring");
                break;
            case 2:
                unlockWallSlide = true;
                UI.EnableWallSlide();
                BroadcastMessage("UnlockWallSlide");
                // BroadcastMessage("Summer");
                break;
            case 3:
                unlockWallJump = true;
                UI.EnableWallJump();
                // BroadcastMessage("Autumn");
                break;
            case 4:
                BroadcastMessage("UnlockAxe");
                UI.EnableAxe();
                break;
            case 5:
                BroadcastMessage("UnlockSword");
                break;
            case 6:
                BroadcastMessage("Heal", 1);
                break;
        }
    }
}
