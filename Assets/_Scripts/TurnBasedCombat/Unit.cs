using System.Security.Cryptography;
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
    public PlayerStats ps;

    private void Start()
    {
        if(gameObject.tag == "Player" && ps != null && ps.Stats.Values.Count != 0)
        {
            brawn = ps.Stats["BrawnText"];
            agility = ps.Stats["AgilityText"];
            endurance = ps.Stats["EnduranceText"];
            wisdom = ps.Stats["WisdomText"];
            intellect = ps.Stats["KnowledgeText"];
            charm = ps.Stats["CharmText"];
            maxHP= ps.health;
            curHP = maxHP;
        }
    }
    public int takeDamage(int damage, bool crit)
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
