using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeUIController : MonoBehaviour
{   
    Slider mVol;
    public AudioMixer master;
    [SerializeField]string volParam;
    void  Start()
    {
        if(mVol == null)
        {
            mVol = GetComponent<Slider>();
        }
        master.GetFloat(volParam,out float f);
        f = Mathf.Pow(10, f / 80);
        mVol.value= f;
    }
    
    public void VolChange()
    {
        
        float vol = mVol.value;
        float newVol = Mathf.Log10(vol)*80;
        master.SetFloat(volParam,newVol);
    }
}
