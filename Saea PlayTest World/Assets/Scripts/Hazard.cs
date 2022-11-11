using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    public Tilemap hazardTileMap;

    private void Start()
    {
        hazardTileMap = GetComponent<Tilemap>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(1);
            GameObject.Find("Player").GetComponent<DeathHandler>().Return();
        }
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(100, Vector3.zero);
        }
    }
}
