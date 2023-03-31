using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnterCloudsReach.Audio
{
public class ButtonSFX : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip bSFX;
    GameObject ButtonSFXSource;
    void Start()
    {   
        ButtonSFXSource = GameObject.Find("ButtonSFX");
        if(ButtonSFXSource != null){
        audioSource = ButtonSFXSource.GetComponent<AudioSource>();
        }
    }
    public void ButtonSFXPlay()
    {   
        
        if(audioSource != null){
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(bSFX);
        }
        }
    }
}
}