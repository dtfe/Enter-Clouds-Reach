using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;

    public int defense;

    public int attackBonus;
    public int damage;

    public int maxHP;
    public int curHP;

    public void takeDamage(int damage)
    {
        curHP -= damage;
    }
}
