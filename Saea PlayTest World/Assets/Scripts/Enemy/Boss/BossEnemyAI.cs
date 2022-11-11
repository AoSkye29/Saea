using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAI : MonoBehaviour
{
    private Transform target;

    private Rigidbody2D rb;

    private Vector3 startPosition;

    private bool engaging = false;

    [SerializeField] private float walkSpeed;
    private bool canMove = true;

    [SerializeField] private bool hasDash;
    private bool canDash;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        if (hasDash == true) canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (engaging == true && canMove == true)
        {
            Vector3 destination = target.transform.position - transform.position;
            float direction = 0;
            if (destination.x > 0)
            {
                direction = 1;
            }
            else if (destination.x < 0)
            {
                direction = -1;
            }
            Flip();
            rb.velocity = new Vector2(walkSpeed * direction, rb.velocity.y);
            if (hasDash == true && canDash == true && destination.x * direction >= 3)
            {
                StartCoroutine(Dash(direction));
            }

        }
    }

    private IEnumerator Dash(float direction)
    {
        canMove = false;
        canDash = false;
        rb.velocity = new Vector2(dashSpeed * direction, 0f);
        yield return new WaitForSeconds(dashTime);
        canMove = true;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Engage()
    {
        engaging = true;
    }

    private void Disengage()
    {
        engaging = false;
        transform.position = startPosition;
    }

    private void Flip()
    {
        Vector3 scaler = transform.localScale;
        if (rb.velocity.x > 0)
        {
            scaler.x = 1;
        }
        else if (rb.velocity.x < 0)
        {
            scaler.x = -1;
        }
        transform.localScale = scaler;
    }

    public void CannotMove()
    {
        canMove = false;
    }

    public void CanMove()
    {
        canMove = true;
    }
}
