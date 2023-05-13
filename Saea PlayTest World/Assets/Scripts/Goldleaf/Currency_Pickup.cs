using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency_Pickup : MonoBehaviour
{
    private Transform player;
    [SerializeField] private bool isCorrupted;

    [SerializeField] private float PickupDelay = 0.5f;

    private void Awake()
    {
        StartCoroutine(StartPickupDelay());
        player = GameObject.Find("Player").transform;
        Physics2D.IgnoreLayerCollision(10, 6, true);
        Physics2D.IgnoreLayerCollision(10, 7, true);
        Physics2D.IgnoreLayerCollision(10, 10, true);
    }

    private IEnumerator StartPickupDelay()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        yield return new WaitForSeconds(PickupDelay);
        Physics2D.IgnoreLayerCollision(10, 9, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<Inventory>().Goldleaf(isCorrupted);
            Destroy(gameObject);
        }
    }
}
