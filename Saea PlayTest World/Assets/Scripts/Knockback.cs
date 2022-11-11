using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float strength = 16f;
    [SerializeField] private float delay = 0.15f;

    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onEnd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PlayFeedback(Vector3 senderPosition)
    {
        onStart?.Invoke();
        StopAllCoroutines();
        Vector2 direction = (transform.position - senderPosition).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        onEnd?.Invoke();
    }
}
