using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonSFX : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip bSFX;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void ButtonSFXPlay()
    { 
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(bSFX);
        }
    }
}
