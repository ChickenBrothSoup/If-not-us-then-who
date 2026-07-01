using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    SMALLARMSFIRE,
    ANTIMATERIALRIFLE,
    PINTLEMOUNTMACHINEGUN,
    FORTYMMFIRE,
    INFANTRYSELECTVOICELINE,
    ENGINEERSELECTVOICELINE,
    TRUCKSELECTVOICELINE,
    LIGHTTANKVOICELINE,
    UICLICK,
    UISETTINGCLICK
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundlist;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume =1)
    {
        instance.audioSource.PlayOneShot(instance.soundlist[(int)sound], volume);
    }

    // Instead of Soundtype, just have a direct audio clip. Instead of soundtype, just give it an audio clip.


}

