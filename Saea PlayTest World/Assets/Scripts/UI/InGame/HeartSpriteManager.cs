using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSpriteManager : MonoBehaviour
{
    [SerializeField] private Image healthBarCurrent;
    [SerializeField] private Image healthBarTotal;

    private PlayerHealth playerHealth;

    private List<float> health = new List<float>();

    private void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        health = playerHealth.GetHealth();
        healthBarTotal.fillAmount = health[0] / 10;
        healthBarCurrent.fillAmount = health[1] / 10;
    }
}
