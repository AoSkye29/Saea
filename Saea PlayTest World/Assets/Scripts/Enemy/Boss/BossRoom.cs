using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [SerializeField] private GameObject roomCamera;
    private GameObject worldCamera;

    [SerializeField] private GameObject exit;

    private bool bossDefeated = false;

    [SerializeField] private GameObject destructible;

    private void Start()
    {
        worldCamera = GameObject.Find("Main Camera");
        destructible.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossDefeated == false)
            {
                BroadcastMessage("Engage");
                exit.SetActive(true);
            }
            roomCamera.SetActive(true);
            worldCamera.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomCamera.SetActive(false);
            worldCamera.SetActive(true);
            exit.SetActive(false);
            if(bossDefeated ==false)
            {
                BroadcastMessage("Disengage");
                BroadcastMessage("Heal");
            }
        }
    }

    public void BossDefeated()
    {
        bossDefeated = true;
        exit.SetActive(false);
    } 
}
