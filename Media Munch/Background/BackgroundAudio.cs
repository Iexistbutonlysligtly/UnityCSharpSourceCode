using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public AudioClip backgroundSound;
    public AudioSource SoundSource;

    void Start()
    {
        SoundSource.clip = backgroundSound;
        SoundSource.Play();


    }

    
    void Update()
    {
        
    }
}
