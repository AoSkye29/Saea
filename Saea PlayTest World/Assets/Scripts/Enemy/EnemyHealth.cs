using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;

    [SerializeField] private float invulnerabilityDuration;
    private bool invincible = false;

    [SerializeField] private UnityEvent<Vector3> onHit;

    [SerializeField] private Transform corruptedGoldleaf;
    [SerializeField] private int minGoldleafDropped;
    [SerializeField] private int maxGoldleafDropped;

    public void TakeDamage(float damage, Vector3 senderPosition)
    {
        if (invincible == false)
        {
            if (senderPosition != Vector3.zero)
            {
                onHit?.Invoke(senderPosition);
            }
            health -= damage;
            CheckHealth();
            StartCoroutine(Invulnerability());
        }
    }

    private IEnumerator Invulnerability()
    {
        invincible = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        invincible = false;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            StopAllCoroutines();
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            transform.GetComponent<EnemyAI>().enabled = false;
            transform.GetComponent<EnemyAttack>().enabled = false;
            GetComponent<Animator>().SetTrigger("Death");
            for(int i = 1; i <= Random.Range(minGoldleafDropped, maxGoldleafDropped + 1); i++)
            {
                Instantiate(corruptedGoldleaf, transform.position, Quaternion.identity);
            }
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
