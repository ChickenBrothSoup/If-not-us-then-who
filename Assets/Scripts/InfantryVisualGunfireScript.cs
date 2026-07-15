using UnityEngine;
using System.Collections;

public class InfantryVisualGunfireScript : MonoBehaviour
{
    [Header("Sprite")]
    public SpriteRenderer spriteRenderer;

    [Header("Random Timing")]
    public float minOnTime = 0.1f;
    public float maxOnTime = 1f;

    public float minOffTime = 0.1f;
    public float maxOffTime = 1f;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // Turn sprite ON
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));

            // Turn sprite OFF
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
        }
    }
}
