using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    private  Traits traits;
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
        traits = GetComponent<Traits>();
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
    
    public void Trait(string traitName)
    {
        PlayerTraits[] traitAr = traits.traitArray;
        int i = 0;
        foreach(PlayerTraits playerTraits in traits.traitArray)
        {
            if (traitName != traitAr[i].traitName)
            {
                i++;
            }
            else
            {
                if (playerStats.Traits.ContainsKey(traitAr[i]))
                {
                    bool traitTF = playerStats.Traits[traitAr[i]];
                    if (traitTF)
                    {
                        playerStats.Traits[traitAr[i]] = false;
                        playerStats.Stats["BrawnText"] -= traitAr[i].brawnModifier;
                        playerStats.Stats["AgilityText"] -= traitAr[i].agilityModifier;
                        playerStats.Stats["EnduranceText"] -= traitAr[i].enduranceModifier;
                        playerStats.Stats["KnowledgeText"] -= traitAr[i].knowledgeModifier;
                        playerStats.Stats["WisdomText"] -= traitAr[i].wisdomModifier;
                        playerStats.Stats["CharmText"] -= traitAr[i].charmModifier;
                        maxTotal -= traitAr[i].totalMod;
                    }
                    else
                    {
                        playerStats.Traits[traitAr[i]] = true;
                        playerStats.Stats["BrawnText"] += traitAr[i].brawnModifier;
                        playerStats.Stats["AgilityText"] += traitAr[i].agilityModifier;
                        playerStats.Stats["EnduranceText"] += traitAr[i].enduranceModifier;
                        playerStats.Stats["KnowledgeText"] += traitAr[i].knowledgeModifier;
                        playerStats.Stats["WisdomText"] += traitAr[i].wisdomModifier;
                        playerStats.Stats["CharmText"] += traitAr[i].charmModifier;
                        maxTotal += traitAr[i].totalMod;
                    }
                }Debug.Log(playerStats.Stats.Values.Sum());
                Debug.Log(maxTotal);
                break;
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
        for(int i = 0; i < traits.traitArray.Length; i++)
        {
            if(!playerStats.Traits[traits.traitArray[i]])
            {
                playerStats.Traits.Remove(traits.traitArray[i]);
            }
        }
        SceneManager.LoadScene(2);
    }
    //AAAAAAAAAAAAAAH 
}
