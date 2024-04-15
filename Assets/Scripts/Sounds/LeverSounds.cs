using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSounds : MonoBehaviour
{
    [SerializeField] private AudioClip action;
    [SerializeField] private GameObject sound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = sound.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1;
        audioSource.volume = 0.1f;
        audioSource.clip = action;
    }

    public void PlayAudio()
    {
        audioSource.PlayDelayed(1-0.31f);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
