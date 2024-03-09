using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    /*I may be wrong in assuming I put my audio functions here. I haven't encountered singletons
    * before. From what I can gather, a singleton is a static single global class which can be referenced
    *by other scripts without having to create it's own instance??
    * Let me know if I have that completely wrong and I'll move these functions elsewhere */


    public void PlaySound(AudioSource audioSource)
    {
        
        audioSource.Play();
    }
}
