using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] d20Sounds;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayD20Sound(int clip)
    {
        audioSource.PlayOneShot(d20Sounds[clip - 1]);
    }
}
