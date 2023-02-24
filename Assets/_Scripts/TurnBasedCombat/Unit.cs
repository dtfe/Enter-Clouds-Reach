using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum statusEffects
{
    Stunned,
    Bleed,
    Poison,
    
}

public class Unit : MonoBehaviour
{
    public string unitName;
    [Header("Stats")]
    public int brawn;
    public int agility;
    public int endurance;
    public int wisdom;
    public int intellect;
    public int charm;

    [Header("Roll Parameters")]
    public int defense;
    public int attackBonus;

    [Header("Damage Parameters")]
    //public int numbOfDice;
    public dieRoller.dFaces damage;
    public int damageBonus;

    [Header("Health Parameters")]
    public int maxHP;
    public int curHP;

    [Header("Status Effects")]
    public bool stunnedImmune;
    public int stunned;

    public bool bleedImmune;
    public int bleed;

    public bool poisonImmune;
    public int poison;

    public int takeDamage(int damage, bool crit)
    {
        if (crit)
        {
            curHP -= damage * 2;
            GetComponentInChildren<BattleHUD>().SetHP(curHP);
            return damage * 2;
        }
        else
        {
            curHP -= damage;
            GetComponentInChildren<BattleHUD>().SetHP(curHP);
            return damage;
        }
    }

    public void addStatus(statusEffects statusToAdd, int amount)
    {
        switch (statusToAdd)
        {
            case statusEffects.Stunned:
                stunned += amount;
                break;

            case statusEffects.Bleed:
                bleed += amount;
                break;

            case statusEffects.Poison:
                poison += amount;
                break;


        }

        GetComponentInChildren<BattleHUD>().refreshStatus(this);
    }
}
