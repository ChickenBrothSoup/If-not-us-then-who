using UnityEngine;

public class MonsterDetectionScript : MonoBehaviour
{

    public enum State { Advancing, Chasing }
    public State CurrentState { get; private set; }

    [Header("Cone Detection")]
    public float coneRange = 10f;
    public float coneAngle = 45f;
    public int rayCount = 8;
    public LayerMask detectionMask;
    public LayerMask obstacleMask;

    [Header("Capture")]
    public float captureRange = 1f;

    private MonsterMovementScript movement;
    private SceneManagerScript sceneTransitionManager;

    private void Awake()
    {
        CurrentState = State.Advancing;
        movement = GetComponent<MonsterMovementScript>();
        sceneTransitionManager = FindFirstObjectByType<SceneManagerScript>();
        InvokeRepeating(nameof(ScanCone), 0f, 0.2f);
    }

    private void Update()
    {
        if (movement == null || movement.Food == null) return;
        if (CurrentState != State.Chasing) return;

        float dist = Vector3.Distance(transform.position, movement.Food.transform.position);
        if (dist < captureRange)
        {
            if (sceneTransitionManager != null)
            {
                sceneTransitionManager.TriggerTransition();
            }
        }
    }

    private void ScanCone()
    {
        if (movement == null || movement.Noise == null) return;

        Vector2 forward = (movement.Noise.transform.position - transform.position).normalized;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-coneAngle, coneAngle, (float)i / (rayCount - 1));
            Vector2 rayDirection = RotateVector(forward, angle);

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, rayDirection, coneRange, detectionMask | obstacleMask);

            if (hit.collider != null)
            {
                if (((1 << hit.collider.gameObject.layer) & detectionMask) != 0)
                {
                    SetState(State.Chasing);
                    return;
                }
            }
        }

        SetState(State.Advancing);
    }

    private void SetState(State newState)
    {
        CurrentState = newState;
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad));
    }

    private void OnDrawGizmos()
    {
        if (movement == null || movement.Noise == null) return;

        Gizmos.color = CurrentState == State.Chasing ? Color.red : Color.yellow;
        Vector2 forward = (movement.Noise.transform.position - transform.position).normalized;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-coneAngle, coneAngle, (float)i / (rayCount - 1));
            Vector2 rayDirection = RotateVector(forward, angle);
            Gizmos.DrawRay(transform.position, rayDirection * coneRange);
        }
    }
}
