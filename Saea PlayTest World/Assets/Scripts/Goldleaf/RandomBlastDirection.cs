using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlastDirection : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    private void ApplyForce()
    {
        //Vector2 direction = new Vector2(Random.Range(-100, 100) / 10, Random.Range(-100, 100) / 10);

        //rb.AddForce(direction, ForceMode2D.Impulse);
        
        rb.AddRelativeForce(Random.onUnitSphere * speed);
    }

    private void Start()
    {
        ApplyForce();
    }
}
