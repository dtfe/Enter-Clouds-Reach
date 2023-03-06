using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerTraits
{
    public string traitName;
    public string traitDesc;
    public bool traitBool;

    public PlayerTraits(string traitName, string traitDesc, bool traitBool)
    {   
        this.traitName = traitName;
        this.traitDesc = traitDesc;
        this.traitBool = traitBool;
    }
}