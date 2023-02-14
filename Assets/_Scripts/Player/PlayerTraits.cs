using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerTraits
{
    public string traitName;
    // public int bMod;
    // public int aMod;
    // public int eMod;
    // public int kMod;
    // public int wMod;
    // public int cMod;
    // public int totalMod;
    public string traitDesc;
    public bool traitBool;

    public PlayerTraits(string traitName, /* int bMod, int aMod, int eMod, int kMod, int wMod, int cMod */ string traitDesc,bool traitBool)
    {   
        //totalMod = bMod+aMod+eMod+kMod+wMod+cMod;
        this.traitName = traitName;
        this.traitDesc = traitDesc;
        // this.bMod = bMod;
        // this.aMod = aMod;
        // this.eMod = eMod;
        // this.kMod = kMod;
        // this.wMod = wMod;
        // this.cMod = cMod;
        this.traitBool = traitBool;
    }
}