using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using EnterCloudsReach.Player;

public class CharacterCreator : MonoBehaviour
{   
    private PlayerStats playerStats;
    private TMP_Text healthText;
    private TMP_Text ACText;
    private TMP_InputField nameInput;
    int pointsLeft;
    PointCounter pointCounter;
    [SerializeField] private TMP_Text[] statText;
    //[SerializeField] private TMP_Text[] traitText;
    [SerializeField] int baseStat = 5;
    [SerializeField]int maxStat = 40;
    [SerializeField]int minStat = 0;
    // bool traitCheck = false;
    GameObject Button;
    [SerializeField] int maxTotal = 40;   
    // Start is called before the first frame update
    void Start()
    { 
        pointCounter = FindObjectOfType<PointCounter>();
        nameInput = FindObjectOfType<TMP_InputField>();
        playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
        Trait[] traits = playerStats.traits;
        //ACText = GameObject.Find("AC").GetComponent<TMP_Text>();
        //healthText = GameObject.Find("Health").GetComponent<TMP_Text>();
        int statLength = statText.Length;
        
        for(int i = 0; i < statLength;i++)
        {
            statText[i].SetText(baseStat.ToString());
            if(!playerStats.Stats.ContainsKey(statText[i].name))
            {
                playerStats.Stats.Add(statText[i].name,baseStat);
            }
            else
            {
                playerStats.Stats[statText[i].name] = baseStat;
            }
            Debug.Log(playerStats.Stats.Values.Count);
        }
        int j = 0;
        foreach(Trait t in traits)
        {   
            Debug.Log(traits[j].playerTraits.traitName);
            j++;
        }
        playerStats.playerName = "Hero";
        nameInput.SetTextWithoutNotify(playerStats.playerName);
        pointsLeft = maxTotal - playerStats.Stats.Values.Sum();
        pointCounter.pointLeft.SetText(pointsLeft.ToString());
    }
    // void Update()
    // {
    //     setHealth();
    //     setAc();
    // }
    public void posIncrement(TMP_Text posText)
    {//setHealth();
        if(playerStats.Stats.ContainsKey(posText.name) && playerStats.Stats.Values.Sum() < maxTotal && playerStats.Stats[posText.name] < maxStat)
        {
            
            playerStats.Stats[posText.name] = playerStats.Stats[posText.name]+1;
            posText.SetText(playerStats.Stats[posText.name].ToString());
            pointsLeft--;
            ChangePointText();
        }
    }
    public void negIncrement(TMP_Text negText)
    {   //setHealth();
        if(playerStats.Stats.ContainsKey(negText.name) && playerStats.Stats[negText.name] > minStat)
        {
            
            playerStats.Stats[negText.name] = playerStats.Stats[negText.name]-1;
            negText.SetText(playerStats.Stats[negText.name].ToString());
            pointsLeft++;
            ChangePointText();
        }
    }
    
//    void setHealth()
//     {
//         int health = playerStats.GetBonus("EnduranceText")*10;
//         if(health <= 0)
//         {
//             health = 10;
//         }
//         playerStats.health = health;
//         healthText.SetText(health.ToString());
//     }
    // void setAc()
    // {
    //     int Ac = 10 + playerStats.GetBonus("AgilityText");
    //     playerStats.AC = Ac;
    //     ACText.SetText(Ac.ToString());
    // }
    public void Trait(string traitName)
    {   
        Button = GameObject.Find(traitName);
        Trait[] traits = playerStats.traits;
        int i = 0;
        foreach(Trait t in traits)
        {
            bool traitTF = traits[i].playerTraits.traitBool;
            if (traitTF && traits[i].playerTraits.traitName == traitName)
            {   
                //negTrait(traitAr[i]);
                buttonImage(Button);
                traits[i].playerTraits.traitBool = false;
                //addTraitStats();
                break;
            }
            else if(!traitTF && traits[i].playerTraits.traitName == traitName)
            {
                //posTrait(traitAr[i]);
                traits[i].playerTraits.traitBool = true;
                buttonImage(Button);
                //addTraitStats();
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
    
    public void nameChange()
    {
        playerStats.playerName = nameInput.text;
    }
    public void nextScene(string scene)
    {
        playerStats.health = 11;
        Debug.Log(playerStats.Stats.Values.Count);
        SceneManager.LoadScene(scene);
    }

    public void ChangePointText()
    {
        pointCounter.pointLeft.SetText(pointsLeft.ToString());
    }

    // //AAAAAAAAAAAAAAH 
    // void negTrait(PlayerTraits traits)
    // {      
    //     if(!traitCheck){
    //         playerStats.Stats["BrawnText"] -= traits.bMod;
    //         playerStats.Stats["AgilityText"] -= traits.aMod;
    //         playerStats.Stats["EnduranceText"] -= traits.eMod;            
    //         playerStats.Stats["KnowledgeText"] -= traits.kMod;
    //         playerStats.Stats["WisdomText"] -= traits.wMod;
    //         playerStats.Stats["CharmText"] -= traits.cMod;
    //         maxStats[0] -= traits.bMod;
    //         maxStats[1] -= traits.aMod;
    //         maxStats[2] -= traits.eMod;            
    //         maxStats[3] -= traits.kMod;
    //         maxStats[4] -= traits.wMod;
    //         maxStats[5] -= traits.cMod;
    //         maxTotal -= traits.totalMod;
    //         if(playerStats.Stats.Values.Min() < 0 )
    //         {
    //             playerStats.Stats["BrawnText"] += traits.bMod;
    //             playerStats.Stats["AgilityText"] += traits.aMod;
    //             playerStats.Stats["EnduranceText"] += traits.eMod;            
    //             playerStats.Stats["KnowledgeText"] += traits.kMod;
    //             playerStats.Stats["WisdomText"] += traits.wMod;
    //             playerStats.Stats["CharmText"] += traits.cMod;
    //             maxStats[0] += traits.bMod;
    //             maxStats[1] += traits.aMod;
    //             maxStats[2] += traits.eMod;            
    //             maxStats[3] += traits.kMod;
    //             maxStats[4] += traits.wMod;
    //             maxStats[5] += traits.cMod;
    //             maxTotal += traits.totalMod;
    //             traitCheck = true;
    //             return;
    //         }
    //         }
    //         traitCheck = false;

    // }
    // void posTrait(PlayerTraits traits)
    // {   
    //     if(!traitCheck){
    //     playerStats.Stats["BrawnText"] += traits.bMod;
    //     playerStats.Stats["AgilityText"] += traits.aMod;
    //     playerStats.Stats["EnduranceText"] += traits.eMod;            
    //     playerStats.Stats["KnowledgeText"] += traits.kMod;
    //     playerStats.Stats["WisdomText"] += traits.wMod;
    //     playerStats.Stats["CharmText"] += traits.cMod;
    //     maxStats[0] += traits.bMod;
    //     maxStats[1] += traits.aMod;
    //     maxStats[2] += traits.eMod;            
    //     maxStats[3] += traits.kMod;
    //     maxStats[4] += traits.wMod;
    //     maxStats[5] += traits.cMod;
    //     maxTotal += traits.totalMod;
    //     if(playerStats.Stats.Values.Min() < 0 )
    //     {
    //         playerStats.Stats["BrawnText"] -= traits.bMod;
    //         playerStats.Stats["AgilityText"] -= traits.aMod;
    //         playerStats.Stats["EnduranceText"] -= traits.eMod;            
    //         playerStats.Stats["KnowledgeText"] -= traits.kMod;
    //         playerStats.Stats["WisdomText"] -= traits.wMod;
    //         playerStats.Stats["CharmText"] -= traits.cMod;
    //         maxStats[0] -= traits.bMod;
    //         maxStats[1] -= traits.aMod;
    //         maxStats[2] -= traits.eMod;            
    //         maxStats[3] -= traits.kMod;
    //         maxStats[4] -= traits.wMod;
    //         maxStats[5] -= traits.cMod;
    //         maxTotal -= traits.totalMod;
    //         traitCheck = true;
    //         return;
    //     }
    //     }traitCheck = false;
    // }
    // void addTraitStats()
    // {
    //     int i = 0;
    //     foreach(TMP_Text text in statText)
    //     {
    //         statText[i].SetText(playerStats.Stats[statText[i].name].ToString());
    //         i++;
    //     }

    // }
}
