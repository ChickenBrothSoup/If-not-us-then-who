using UnityEngine;

public class HeadquartersMovementScript : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5f;
    public float slowDownRange = 2f;
    public float stopDistance = 0.1f;

    private static Vector3 savedPosition;
    private static bool hasSavedPosition = false;

    private void Awake()
    {
        if (hasSavedPosition)
        {
            transform.position = savedPosition;
        }
    }

    private void OnDestroy()
    {
        savedPosition = transform.position;
        hasSavedPosition = true;
    }

    private void Update()
    {
        if (target == null) return;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= stopDistance)
        {
            this.enabled = false;
            return;
        }

        float speed = Mathf.Clamp(dist / slowDownRange, 0.1f, 1f) * maxSpeed;
        transform.position = Vector3.MoveTowards(
            transform.position, target.position, speed * Time.deltaTime);
    }
}
