using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.transform.GetComponent<BossEnemyHealth>() != null)
            {
                other.transform.GetComponent<BossEnemyHealth>().TakeDamage(damage, transform.position);
            }
            else
            {
                other.transform.GetComponent<EnemyHealth>().TakeDamage(damage, transform.position);
            }
        }
    }
}
