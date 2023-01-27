using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public IDictionary<string,int> Stats = new Dictionary<string, int>();
    public IDictionary<string,bool> Traits = new Dictionary<string,bool>();
}