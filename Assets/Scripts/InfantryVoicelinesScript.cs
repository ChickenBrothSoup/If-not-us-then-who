using System;
using UnityEngine;
using UnityEngine.Audio;

public class InfantryVoicelinesScript : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip voiceSound;
    public bool selected = false;
    public float selectCooldown = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void UnitSelected()
    {
        if (audioSource != null && voiceSound != null && selected == false)
        {
            selected = true;
            audioSource.PlayOneShot(voiceSound);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
