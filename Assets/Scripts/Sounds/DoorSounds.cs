using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSounds : MonoBehaviour
{
    [SerializeField] private AudioClip open;
    [SerializeField] private AudioClip close;
    private AudioSource audioSource;
    private bool isOpenActive = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0;
    }

    public void ToggleAudio()
    {
        if (isOpenActive)
        {
            audioSource.clip = close;
        }
        else
        {
            audioSource.clip = open;
        }
        isOpenActive = !isOpenActive;
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
