using UnityEngine;
using UnityEngine.Events;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _clicked;

    private MouseInputScript _mouse;

    private void Awake()
    {
        _mouse = FindFirstObjectByType<MouseInputScript>();
        _mouse.Clicked += MouseOnClicked;
    }

    private void MouseOnClicked()
    {
        _clicked?.Invoke();
    }
}
