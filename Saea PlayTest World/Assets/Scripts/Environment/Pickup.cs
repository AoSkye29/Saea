using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Transform player;
    [SerializeField] private int id;

    private bool pickedUp = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickedUp == false)
            {
                GetComponent<Animator>().SetBool("Collision", true);
                player.GetComponent<CharacterController2D>().Pickup(id);
                pickedUp = true;
            }
        }
        
    }

    private void OnPickup()
    {
        Destroy(gameObject);
    }
}
