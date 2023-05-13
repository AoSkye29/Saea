using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    [SerializeField] private Transform instructions;

    public void ConvertMoney()
    {
        GameObject.Find("Player").GetComponent<Inventory>().ConvertCorruptedGoldleaf();
        Debug.Log("Attempting goldleaf conversion");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 9)
        {
            instructions.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 9)
        {
            instructions.gameObject.SetActive(false);
        }
    }
}
