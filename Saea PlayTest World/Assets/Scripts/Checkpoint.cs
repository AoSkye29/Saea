using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Player").transform.GetComponent<DeathHandler>().SetCheckpoint(transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);

        BoxCollider2D collider = transform.GetComponent<BoxCollider2D>();
        Vector3 position = new Vector3(transform.position.x + collider.offset.x, transform.position.y + collider.offset.y, transform.position.z);

        Gizmos.DrawCube(position, transform.GetComponent<BoxCollider2D>().size);
    }
}
