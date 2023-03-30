using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Trait[] traits;
    public int AC;
    public int health;
    public Dictionary<string,int> Stats = new Dictionary<string, int>();

    public PlayerTraits GetPlayerTrait(string trait)
    {
        int i = 0;
        foreach(Trait t in traits)
        {   
            if(traits[i].playerTraits.traitName == trait)
            {
                return traits[i].playerTraits;
            }
            i++;
        }
        return traits[0].playerTraits;
    }

    public int GetStat(string stat)
    {
        return Stats[stat];
    }

    public int GetBonus(string stat)
    {
        int statNum = Stats[stat];
        int bonus = statNum-5;
        return bonus;
    }
}