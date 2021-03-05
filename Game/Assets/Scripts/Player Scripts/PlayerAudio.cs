using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] warpSounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWarp(int clip)
    {
        audioSource.PlayOneShot(warpSounds[clip - 1]);
    }
}
