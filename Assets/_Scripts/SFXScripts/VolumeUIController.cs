using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeUIController : MonoBehaviour
{   
    Slider mVol;
    public AudioMixer master;
    void  Start()
    {
        if(mVol == null)
        {
            mVol = GetComponent<Slider>();
        }
    }
    
    public void VolChange(string volParam)
    {
        
        float vol = mVol.value;
        float newVol = Mathf.Log10(vol)*80;
        master.SetFloat(volParam,newVol);
    }
}
