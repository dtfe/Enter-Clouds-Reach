using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterCreator : TraitsC
{
    public PlayerStats playerStats;
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
    
    public void Trait()
    {
        
        if (playerStats.Traits.ContainsKey(testTrait))
        {
            bool traitTF = playerStats.Traits[testTrait];
            if (!traitTF)
            { 
                playerStats.Traits[testTrait] = true;
            }
            else if(traitTF)
            {
                playerStats.Traits[testTrait] = false;
            }
            
       }
    }
    public Sprite traitChecked;
    public Sprite traitUnChecked;
    public void buttonImage(GameObject button)
    {
        if(button.GetComponent<Image>().sprite == traitChecked)
            {button.GetComponent<Image>().sprite = traitUnChecked;}
        else {button.GetComponent<Image>().sprite = traitChecked;}
        
    }  
    public void nextScene()
    {
        for(int i = 0; i < playerStats.Traits.Count; i++)
        {
            if(!playerStats.Traits[testTrait])
            {
                playerStats.Traits.Remove(testTrait);
            }
        }
        SceneManager.LoadScene(1);
    }
    //AAAAAAAAAAAAAAH 
}
