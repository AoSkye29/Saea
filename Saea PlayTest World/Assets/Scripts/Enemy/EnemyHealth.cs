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
            transform.GetComponent<EnemyAI>().enabled = false;
            transform.GetComponent<EnemyAttack>().enabled = false;
            GetComponent<Animator>().SetTrigger("Death");
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
