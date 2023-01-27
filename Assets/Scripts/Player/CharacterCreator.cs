
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterCreator : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField]private GameObject checkmark;
    [SerializeField] private TMP_Text[] statText;
    [SerializeField] private TMP_Text[] traitText;
    [SerializeField] int baseStat = 5;
    [SerializeField] int maxStat = 10;
    [SerializeField] int minStat = 1;
    [SerializeField] int maxTotal = 40; 
    // Start is called before the first frame update
    void Start()
    {
        int statLength = statText.Length;
        int traitLength = traitText.Length;
        for(int i = 0; i < statLength;i++)
        {
            statText[i].SetText(baseStat.ToString());
            playerStats.Stats.Add(statText[i].name,baseStat);
            Debug.Log(statText[i]);
            Debug.Log(playerStats.Stats.Values.Sum());
        }
        for(int j = 0; j < traitLength; j++)
        {
            playerStats.Traits.Add(traitText[j].name, false);
        }
    }
    public void posIncrement(TMP_Text posText)
    {
        if(playerStats.Stats.ContainsKey(posText.name) && playerStats.Stats.Values.Sum() < maxTotal && playerStats.Stats[posText.name] < maxStat)
        {
            playerStats.Stats[posText.name]++;
            posText.SetText(playerStats.Stats[posText.name].ToString());
            Debug.Log(playerStats.Stats.Values.Sum());
        }
    }
    public void negIncrement(TMP_Text negText)
    {
        if(playerStats.Stats.ContainsKey(negText.name) && playerStats.Stats[negText.name] > minStat)
        {
            playerStats.Stats[negText.name]--;
            negText.SetText(playerStats.Stats[negText.name].ToString());
            Debug.Log(playerStats.Stats.Values.Sum());
        }
    }
    public void Trait(TMP_Text traitText)
    {
       if (playerStats.Traits.ContainsKey(traitText.name))
       {
        bool traitTF = playerStats.Traits[traitText.name];
            if (!traitTF)
            { 
                playerStats.Traits[traitText.name] = true;
                checkmark.SetActive(true);
            }
            else if(traitTF)
            {
                checkmark.SetActive(false);
                playerStats.Traits[traitText.name] = false;
            }
            
       }
        
    }
        
    public void nextScene()
    {
        SceneManager.LoadScene(1);
    }
    //AAAAAAAAAAAAAAH 
}
