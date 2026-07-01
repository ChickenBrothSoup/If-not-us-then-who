using UnityEngine;

public class AppearOnBattleFieldScript : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5f;
    public float slowDownRange = 2f;
    public float stopDistance = 0.1f;

    private MovementScript movementScript;

    private void Awake()
    {
        movementScript = GetComponent<MovementScript>();

        if (movementScript != null)
        {
            movementScript.enabled = false;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist <= stopDistance)
        {
            // Snap exactly to target
            transform.position = target.position;

            if (movementScript != null)
            {
                movementScript.enabled = true;
            }

            enabled = false;
            return;
        }

        float speed = maxSpeed;

        if (dist < slowDownRange)
        {
            speed = Mathf.Lerp(0f, maxSpeed, dist / slowDownRange);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
    }
}
