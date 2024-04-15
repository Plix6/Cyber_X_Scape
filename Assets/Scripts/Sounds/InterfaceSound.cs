using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSound : MonoBehaviour
{
    [SerializeField] private AudioClip open;
    [SerializeField] private AudioClip close;
    private AudioSource audioSource;
    private bool isOpenActive = false;

    private void Awake()
    {
        audioSource = gameObject.GetComponentInChildren<AudioSource>();
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
