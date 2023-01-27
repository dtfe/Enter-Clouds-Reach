using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField]
    TMP_Text[] statText;
    void Start()
    {
        for(int i = 0; i < statText.Length; i++)
        {
            if(playerStats.Stats.ContainsKey(statText[i].name)){
                statText[i].SetText(playerStats.Stats[statText[i].name].ToString());
            }
            else Debug.Log(playerStats.Stats.Keys.ToString());
            Debug.Log(statText[i]);
        }
    }
}
