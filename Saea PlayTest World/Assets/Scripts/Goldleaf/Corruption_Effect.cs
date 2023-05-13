using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corruption_Effect : MonoBehaviour
{
    [SerializeField] private Sprite standard;
    [SerializeField] private List<Sprite> corruption_Effects;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int maxDelay;

    private void Start()
    {
        StartCoroutine(Play_Effect());
    }

    private IEnumerator Play_Effect()
    {
        int effects = Random.Range(1, 4);
        int lastEffect = 0;
        for (int i = 1; i <= effects; i++)
        {
            int effect = lastEffect;
            while (effect == lastEffect)
            {
                effect = Random.Range(1, corruption_Effects.Count + 1);
            }
            spriteRenderer.sprite = corruption_Effects[effect - 1];
            lastEffect = effect;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.sprite = standard;
        int delay = Random.Range(1, maxDelay);
        yield return new WaitForSeconds(delay);
        StartCoroutine(Play_Effect());
    }
}
