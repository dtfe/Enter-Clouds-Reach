using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortsword : ScriptableObject
{
    public void attack1()
    {
        string attackName = "Careful Slash";
        int attackBonus = 4;
        DieRoller.dFaces damageDie = DieRoller.dFaces.d4;
        int damageBonus = 0;
    }
    public void attack2()
    {
        string attackName = "Lunging Strike";
        int attackBonus = 2;
        DieRoller.dFaces damageDie = DieRoller.dFaces.d6;
        int damageBonus = 1;
    }
    public void attack3()
    {
        string attackName = "Precise Stab";
        int attackBonus = 0;
        DieRoller.dFaces damageDie = DieRoller.dFaces.d8;
        int damageBonus = 4;
    }
    public void attack4()
    {
        string attackName = "Pommel Jab";
        int attackBonus = 0;
        DieRoller.dFaces damageDie = DieRoller.dFaces.d4;
        int damageBonus = 0;
    }
}
