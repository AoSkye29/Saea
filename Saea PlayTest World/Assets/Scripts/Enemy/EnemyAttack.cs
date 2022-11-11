using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float damage = 1.0f;

    [SerializeField] private Collider2D attackHitbox;
    [SerializeField] private LayerMask targetLayer;

    private float attackCooldown = 1f;
    private bool canAttack = true;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (attackHitbox.IsTouchingLayers(targetLayer))
        {
            if (canAttack == true)
            {
                target.GetComponent<PlayerHealth>().TakeDamageWithKnockback(damage, transform.position);
                StartCoroutine(Cooldown());
            }
        }
    }

    public IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
