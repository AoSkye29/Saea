using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 lastCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        if (lastCheckpoint != checkpointPosition)
        {
            lastCheckpoint = checkpointPosition;
        }
    }

    public void Return()
    {
        transform.position = lastCheckpoint;
    }

    public void Death()
    {
        lastCheckpoint = startPosition;
        transform.position = startPosition;
        Debug.Log("Heal");
        transform.GetComponent<PlayerHealth>().Heal(10);
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>(true);
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.gameObject.SetActive(true);
        }
    }
}
