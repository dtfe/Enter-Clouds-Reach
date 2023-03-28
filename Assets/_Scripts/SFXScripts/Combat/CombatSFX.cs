using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using EnterCloudsReach.Combat;


[RequireComponent(typeof(AudioSource))]
public class CombatSFX : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip aHit;
    public AudioClip aMiss;
    public AudioClip aCritHit;
    public AudioClip ambNoise;
    public AudioClip poisonSFX;
    public AudioClip bleedSFX;
    public AudioClip stunSFX;
    void Start() 
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlayHitAudio(HitTiming ht)
    {
        switch(ht)
        {
            case HitTiming.Miss: 
                audioSource.PlayOneShot(aMiss);
                break;
            case HitTiming.Hit:
                audioSource.PlayOneShot(aHit);
                break;
            case HitTiming.Critical:
                audioSource.PlayOneShot(aCritHit);
                break;
            default:
                break;
        }
    }
    public void AmbientNoises() => audioSource.PlayOneShot(ambNoise);

    public bool CheckIfAudioPlay
    {
        get
        {
            if (audioSource.isPlaying)
            {
                return true;
            }
            return false;
        }
    }
    public void StatusAudio()
    {
        
    }
}
