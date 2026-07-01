using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float slowDownRange = 2f;
    public float stopDistance = 0.1f;

    private Vector3 targetPosition;
    private Camera mainCamera;
    private static Vector3 savedPosition;
    private static bool hasSavedPosition = false;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (hasSavedPosition)
        {
            transform.position = savedPosition;
        }

        targetPosition = transform.position;
    }

    private void OnDestroy()
    {
        savedPosition = transform.position;
        hasSavedPosition = true;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f;
            targetPosition = mousePos;
        }

        float dist = Vector3.Distance(transform.position, targetPosition);
        if (dist <= stopDistance) return;

        float speed = Mathf.Clamp(dist / slowDownRange, 0.1f, 1f) * maxSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
