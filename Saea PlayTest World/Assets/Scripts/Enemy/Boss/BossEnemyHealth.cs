using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossEnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private float invulnerabilityDuration;
    private bool invincible = false;

    [SerializeField] private UnityEvent<Vector3> onHit;

    [SerializeField] private Transform corruptedGoldleaf;
    [SerializeField] private int minGoldleafDropped;
    [SerializeField] private int maxGoldleafDropped;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 senderPosition)
    {
        if (invincible == false)
        {
            if (senderPosition != Vector3.zero)
            {
                onHit?.Invoke(senderPosition);
            }
            currentHealth -= damage;
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
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            transform.GetComponent<BossEnemyAI>().enabled = false;
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
        transform.parent.GetComponent<BossRoom>().BossDefeated();
        Destroy(gameObject);
    }

    private void Heal()
    {
        currentHealth = maxHealth;
    }
}