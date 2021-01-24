using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mySoundManager : MonoBehaviour
{
    public AudioSource effectsSource;
    public AudioSource musicSource;

    public static mySoundManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void Play(AudioClip clip)
    {
        effectsSource.clip = clip;
        effectsSource.Play();
    }

    public void MusicPlay(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }




}
