using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum statusEffects
{
    None,
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

    [Header("Timing Parameters")]
    public GameObject defendTiming;

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

    public int takeDamage(int damage)
    {
        curHP -= damage;
        GetComponentInChildren<BattleHUD>().SetHP(curHP);
        return damage;
    }

    public void addStatus(statusEffects statusToAdd, int amount)
    {
        switch (statusToAdd)
        {
            default:
                break;

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
