using System;
using UnityEngine;

public class HumanUnitMovementScript : MonoBehaviour, IClickable
{
    public bool IsInteractable = true;

    public bool Selected = false;

    
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


