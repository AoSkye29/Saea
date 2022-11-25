using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 5.0f;
    private float currentHealth;

    [SerializeField] private float invulnerabilityDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private List<Transform> playerSprites;
    private bool invincible = false;

    [SerializeField] private UnityEvent<Vector3> onHit;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamageWithKnockback(float damage, Vector3 senderPosition)
    {
        if (invincible == false)
        {
            while (damage > 0 && currentHealth > 0)
            {
                currentHealth--;
                damage--;
            }
            CheckHealth();
            if (currentHealth != maxHealth)
            {
                
                StartCoroutine(Invulnerability());
            }
        }
        if (currentHealth != maxHealth)
        {
            onHit?.Invoke(senderPosition);
        }
    }

    public void TakeDamage(float damage)
    {
        if (invincible == false)
        {
            while (damage > 0 && currentHealth > 0)
            {
                currentHealth--;
                damage--;
            }
            CheckHealth();
            if (currentHealth != maxHealth)
            {
                StartCoroutine(Invulnerability());
            }
        }
    }

    public void Heal(float heal)
    {
        while (heal > 0)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth++;
            }
            heal--;
        }
    }

    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            transform.GetComponent<DeathHandler>().Death();
        }
    }

    private IEnumerator Invulnerability()
    {
        invincible = true;
        for (int i = 1; i <= numberOfFlashes; i++)
        {
            ChangeSprite(0.5f);
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
            ChangeSprite(1f);
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
        }
        invincible = false;
    }

    private void ChangeSprite(float transparency)
    {
        foreach (Transform sprite in playerSprites)
        {
            sprite.GetComponent<SpriteRenderer>().color = new Color(1 , 1, 1, transparency);
        }
    }

    public List<float> GetHealth()
    {
        return new List<float> {maxHealth, currentHealth};
    }
}
