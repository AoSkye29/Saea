using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;

    private bool facingLeft = false;
    private float facingDirection = 1;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 groundCheckSize;
    private bool isGrounded = false;
    [SerializeField] private Transform edgeCheck;
    [SerializeField] private Vector3 edgeCheckSize;
    private bool isOnEdge = false;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsEnemy;
    private LayerMask collisionLayers;

    private bool mustPatrol = true;
    private bool canMove = true;
    [SerializeField] private float walkSpeed;
    private Rigidbody2D rb;
    [SerializeField] private Collider2D physicsCollider;

    private bool justRotated = false;
    [SerializeField] private float rotationCooldown;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        collisionLayers = whatIsGround | whatIsEnemy;
        Physics2D.IgnoreCollision(physicsCollider, target.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, whatIsGround);
        isOnEdge = !Physics2D.OverlapBox(edgeCheck.position, edgeCheckSize, 0f, whatIsGround);

        if ((isOnEdge == true && isGrounded == true) || bodyCollider.IsTouchingLayers(collisionLayers))
        {
            Rotate();
        }

        if (mustPatrol == true && canMove == true)
        {
            rb.velocity = new Vector2(walkSpeed * facingDirection, rb.velocity.y);
        }
    }

    private void Rotate()
    {
        if (justRotated == false)
        {
            facingLeft = !facingLeft;
            facingDirection *= -1;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
            StartCoroutine(RotationCooldown());
        }
    }

    public void changeCanMove()
    {
        canMove = !canMove;
    }

    private IEnumerator RotationCooldown()
    {
        justRotated = true;
        yield return new WaitForSeconds(rotationCooldown);
        justRotated = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawCube(groundCheck.position, groundCheckSize);

        Gizmos.color = Color.red;

        Gizmos.DrawCube(edgeCheck.position, edgeCheckSize);
    }
}
