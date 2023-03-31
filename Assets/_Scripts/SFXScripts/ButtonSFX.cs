using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    public AudioClip bSFX;
    
    public void ButtonSFXPlay()
    { 
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(bSFX);
        }
    }
}
