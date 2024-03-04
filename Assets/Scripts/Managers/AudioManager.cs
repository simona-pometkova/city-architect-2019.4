using Assets;
using Assets.Scripts;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonClickSound;

    public void PlayButtonClickSound()
    {
        _audioSource.clip = _buttonClickSound;
        _audioSource.Play();
    }
}
