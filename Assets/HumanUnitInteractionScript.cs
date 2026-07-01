using UnityEngine;


public class HumanUnitInteractionScript : MonoBehaviour, IClickable
{  
    public void OnClicked()
    {
        SoundManager.PlaySound(SoundType.UICLICK);

    }
}
