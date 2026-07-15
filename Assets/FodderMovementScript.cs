using UnityEngine;

public class FodderMovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 0.1f;
    [SerializeField] private bool destroyWhenTargetReached = true;

    private Transform target;

    // The spawner uses this method to give the enemy its target.
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        // Do nothing when no target has been assigned.
        if (target == null)
        {
            return;
        }

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.position;

        float distanceToTarget =
            Vector2.Distance(currentPosition, targetPosition);

        // Check whether the enemy has reached the player's side.
        if (distanceToTarget <= stopDistance)
        {
            if (destroyWhenTargetReached)
            {
                Destroy(gameObject);
            }

            return;
        }

        // Find the direction from the enemy to the target.
        Vector2 direction =
            (targetPosition - currentPosition).normalized;

        // Move the enemy toward the target.
        transform.position +=
            (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
