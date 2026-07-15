using UnityEngine;

public class MonsterApproachingSpriteScript : MonoBehaviour
{
    [Header("Growth Settings")]
    public Vector3 targetScale = new Vector3(3f, 3f, 1f);
    public float growthSpeed = 1f; // Scale units per second

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            targetScale,
            growthSpeed * Time.deltaTime
        );
    }
}
