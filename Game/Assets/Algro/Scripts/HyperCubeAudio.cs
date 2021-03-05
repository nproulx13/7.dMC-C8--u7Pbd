using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCubeAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip active;
    public AudioClip inactive;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHyperCubeActive()
    {
        audioSource.clip = active;
        audioSource.Stop();
        audioSource.Play();
    }

    public void PlayHyperCubeInactive()
    {
        audioSource.clip = inactive;
        audioSource.Stop();
        audioSource.Play();
    }

}
