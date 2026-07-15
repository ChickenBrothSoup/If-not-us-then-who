using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanUnitMovementScript : MonoBehaviour, IClickable
{
    public bool IsInteractable = true;

    public bool Selected = false;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public LayerMask groundLayer;

    private bool isMoving = false;
    private Vector3 targetPosition;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    public void InfantryClicked()
    {
        if (IsInteractable == true)
        {
            Selected = true;
            SoundManager.PlaySound(SoundType.UICLICK);
            SoundManager.PlaySound(SoundType.INFANTRYSELECTVOICELINE);
        }
        Debug.Log("Pencil And Paper");
        return;
    }

    public void MoveCommand()
    {


        Debug.Log("MoveCommand called"); // Is this appearing?

        if (!IsInteractable || !Selected)
        {
            Debug.Log($"Blocked — IsInteractable: {IsInteractable}, Selected: {Selected}");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Debug.Log($"Hit: {hit.collider.gameObject.name} at {hit.point}");
            targetPosition = hit.point;
            isMoving = true;
        }
        else
        {
            Debug.Log("Raycast hit nothing — check groundLayer mask");
        }
    }

    public void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                isMoving = false;
                Selected = false; // deselect on arrival, remove if you don't want this
            }
        }
    }



    public void EngineerClicked()
    {

    }

    public void SupplyTruckClicked()
    {

    }

    public void LightTankClicked()
    {

    }
}


