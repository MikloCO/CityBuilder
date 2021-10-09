using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    private static Audiomanager _instance;

    public static Audiomanager Instance { get => _instance; }

    public AudioClip buttonClickedSound, placeBuildingSound, removeBuildingSound;
    public AudioSource effectAudioSource;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    public void PlayButtonClickedSound()
    {
        effectAudioSource.Stop();
        effectAudioSource.clip = buttonClickedSound;
        effectAudioSource.Play();
    }

    internal void PlayDemolishionSound()
    {
        effectAudioSource.Stop();
        effectAudioSource.clip = removeBuildingSound;
        effectAudioSource.Play();
    }

    internal void PlayPlaceBuildingSound()
    {
        effectAudioSource.Stop();
        effectAudioSource.clip = placeBuildingSound;
        effectAudioSource.Play();
    }
}
