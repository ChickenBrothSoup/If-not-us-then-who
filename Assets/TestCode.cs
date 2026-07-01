using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestCode : MonoBehaviour
{
    Image image;

    Sprite sprite;

    //private void Update()
    //{
    //    if(Input.GetButtonDown("Fire1"))
    //    {
    //        Debug.Log("left click works");
    //    }
    //}

    private void OnMouseDown()
    {
        Debug.Log( gameObject.name + " is clicked");
    }
}
