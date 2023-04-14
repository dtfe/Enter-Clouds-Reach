using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Player{
[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Trait[] traits;
    public string playerName = new string("Hero");
    public int AC;
    public int health;
    public Dictionary<string,int> ModStats = new Dictionary<string, int>();
    public Dictionary<string,int> BaseStats = new Dictionary<string, int>();
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

    public int GetBaseStat(string stat)
    {
        return BaseStats[stat];
    }

    public int GetBonus(string stat)
    {
        int statNum = ModStats[stat];
        int bonus = statNum-5;
        return bonus;
    }
}}