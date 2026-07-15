using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputScript : MonoBehaviour
{
    public Vector2 WorldPosition {  get; private set; }
    public event Action Clicked;

    private void OnLook(InputValue value)
    {
        WorldPosition = (Vector2)Camera.main.ScreenToWorldPoint(position: (Vector3)value.Get<Vector2>());


    }

    private void OnAction(InputValue _)
    {
        Clicked?.Invoke();
    }
}
