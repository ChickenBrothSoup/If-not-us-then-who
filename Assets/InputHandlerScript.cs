using UnityEngine;
using UnityEngine.InputSystem;

public interface IClickable
{
    
    void InfantryClicked();
    void EngineerClicked();

    void SupplyTruckClicked();
    void LightTankClicked();

    
}
public class InputHandlerScript : MonoBehaviour
{

    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        Debug.Log(rayHit.collider.gameObject.name);
        var clickable = rayHit.collider.GetComponent<IClickable>();

        

        clickable?.InfantryClicked();
        
    }
}
