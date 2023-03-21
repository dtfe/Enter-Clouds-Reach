using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public float easyMod, hardMod, insaneMod;
    public float curMod;

    private void Start()
    {
        curMod = Mathf.Clamp(curMod, 0.6f, 1.4f);
    }

}