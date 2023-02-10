using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{   
    public PlayerStats playerStats;
    [SerializeField] private TMP_Text[] statText;
    //[SerializeField] private TMP_Text[] traitText;
    [SerializeField] int baseStat = 5;
    int[] maxStats;
    int[] minStats;
    bool traitCheck = false;
    GameObject Button;
    [SerializeField] int maxTotal = 40;   
    // Start is called before the first frame update
    void Start()
    {   minStats = new int[statText.Length];
        maxStats = new int[statText.Length];
        if(playerStats.traitArray == null)
        {
            playerStats.traitArray = playerStats.GetVariablesOfType<PlayerTraits>();
        }
        int statLength = statText.Length;
        //int traitLength = traitText.Length;
        for(int i = 0; i < statLength;i++)
        {   
            maxStats[i] = 10;
            minStats[i] = 0;
            statText[i].SetText(baseStat.ToString());
            playerStats.Stats.Add(statText[i].name,baseStat);
        }
       
    }
    public void posIncrement(TMP_Text posText)
    {
        if(playerStats.Stats.ContainsKey(posText.name) && playerStats.Stats.Values.Sum() < maxTotal && playerStats.Stats[posText.name] < maxStats[Array.IndexOf(statText,posText)])
        {
            playerStats.Stats[posText.name]++;
            posText.SetText(playerStats.Stats[posText.name].ToString());
        }
    }
    public void negIncrement(TMP_Text negText)
    {
        if(playerStats.Stats.ContainsKey(negText.name) && playerStats.Stats[negText.name] > minStats[0])
        {
            playerStats.Stats[negText.name]--;
            negText.SetText(playerStats.Stats[negText.name].ToString());
        }
    }
    
    public void Trait(string traitName)
    {   
        Button = GameObject.Find(traitName);
        PlayerTraits[] traitAr = playerStats.traitArray;
        int i = 0;
        foreach(PlayerTraits playerTraits in traitAr)
        {
            bool traitTF = traitAr[i].traitBool;
            if (traitTF && traitAr[i].traitName == traitName)
            {   
                negTrait(traitAr[i]);
                buttonImage(Button);
                traitAr[i].traitBool = false;
                addTraitStats();
                break;
            }
            else if(!traitTF && traitAr[i].traitName == traitName)
            {
                posTrait(traitAr[i]);
                traitAr[i].traitBool = true;
                buttonImage(Button);
                addTraitStats();
                break;
            }
            else
            {
                i++;
            }

        }
    }
    public Sprite traitChecked;
    public Sprite traitUnChecked;
    public void buttonImage(GameObject button)
    {
        if (button.GetComponent<Image>().sprite != traitChecked)
        { button.GetComponent<Image>().sprite = traitChecked; }
        else if (button.GetComponent<Image>().sprite != traitUnChecked)
        { button.GetComponent<Image>().sprite = traitUnChecked; }

    }  
    public void nextScene()
    {
        for(int i = 0; i < playerStats.traitArray.Length; i++)
        {
        }
        SceneManager.LoadScene(2);
    }
    //AAAAAAAAAAAAAAH 
    void negTrait(PlayerTraits traits)
    {      
        if(!traitCheck){
            playerStats.Stats["BrawnText"] -= traits.bMod;
            playerStats.Stats["AgilityText"] -= traits.aMod;
            playerStats.Stats["EnduranceText"] -= traits.eMod;            
            playerStats.Stats["KnowledgeText"] -= traits.kMod;
            playerStats.Stats["WisdomText"] -= traits.wMod;
            playerStats.Stats["CharmText"] -= traits.cMod;
            maxStats[0] -= traits.bMod;
            maxStats[1] -= traits.aMod;
            maxStats[2] -= traits.eMod;            
            maxStats[3] -= traits.kMod;
            maxStats[4] -= traits.wMod;
            maxStats[5] -= traits.cMod;
            maxTotal -= traits.totalMod;
            if(playerStats.Stats.Values.Min() < 0 )
            {
                playerStats.Stats["BrawnText"] += traits.bMod;
                playerStats.Stats["AgilityText"] += traits.aMod;
                playerStats.Stats["EnduranceText"] += traits.eMod;            
                playerStats.Stats["KnowledgeText"] += traits.kMod;
                playerStats.Stats["WisdomText"] += traits.wMod;
                playerStats.Stats["CharmText"] += traits.cMod;
                maxStats[0] += traits.bMod;
                maxStats[1] += traits.aMod;
                maxStats[2] += traits.eMod;            
                maxStats[3] += traits.kMod;
                maxStats[4] += traits.wMod;
                maxStats[5] += traits.cMod;
                maxTotal += traits.totalMod;
                traitCheck = true;
                return;
            }
            }
            traitCheck = false;
            
    }
    void posTrait(PlayerTraits traits)
    {   
        if(!traitCheck){
        playerStats.Stats["BrawnText"] += traits.bMod;
        playerStats.Stats["AgilityText"] += traits.aMod;
        playerStats.Stats["EnduranceText"] += traits.eMod;            
        playerStats.Stats["KnowledgeText"] += traits.kMod;
        playerStats.Stats["WisdomText"] += traits.wMod;
        playerStats.Stats["CharmText"] += traits.cMod;
        maxStats[0] += traits.bMod;
        maxStats[1] += traits.aMod;
        maxStats[2] += traits.eMod;            
        maxStats[3] += traits.kMod;
        maxStats[4] += traits.wMod;
        maxStats[5] += traits.cMod;
        maxTotal += traits.totalMod;
        if(playerStats.Stats.Values.Min() < 0 )
        {
            playerStats.Stats["BrawnText"] -= traits.bMod;
            playerStats.Stats["AgilityText"] -= traits.aMod;
            playerStats.Stats["EnduranceText"] -= traits.eMod;            
            playerStats.Stats["KnowledgeText"] -= traits.kMod;
            playerStats.Stats["WisdomText"] -= traits.wMod;
            playerStats.Stats["CharmText"] -= traits.cMod;
            maxStats[0] -= traits.bMod;
            maxStats[1] -= traits.aMod;
            maxStats[2] -= traits.eMod;            
            maxStats[3] -= traits.kMod;
            maxStats[4] -= traits.wMod;
            maxStats[5] -= traits.cMod;
            maxTotal -= traits.totalMod;
            traitCheck = true;
            return;
        }
        }traitCheck = false;
    }
    void addTraitStats()
    {
        int i = 0;
        foreach(TMP_Text text in statText)
        {
            statText[i].SetText(playerStats.Stats[statText[i].name].ToString());
            i++;
        }
        
    }
}
