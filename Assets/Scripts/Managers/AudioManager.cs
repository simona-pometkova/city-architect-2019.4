using Assets;
using Assets.Scripts;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{


    /*I may be wrong in assuming I put my audio functions here[Jerome]. I haven't encountered singletons
    * before. From what I can gather, a singleton is a static single global class which can be referenced
    *by other scripts without having to create it's own instance??
    * Let me know if I have that completely wrong and I'll move these functions elsewhere */


    public void PlaySound(AudioSource audioSource)
    {
        
        audioSource.Play();
    }


    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonClickSound;

    public void PlayButtonClickSound()
    {
        _audioSource.clip = _buttonClickSound;
        _audioSource.Play();
    }
}
