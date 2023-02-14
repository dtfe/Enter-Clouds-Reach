using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;

    public int defense;

    public int attackBonus;

    public DieRoller.dFaces damage;
    public int damageBonus;

    public int maxHP;
    public int curHP;

    public int takeDamage(int damage, bool crit)
    {
        if (crit)
        {
            curHP -= damage * 2;
            return damage * 2;
        }
        else
        {
            curHP -= damage;
            return damage;
        }
    }
}
