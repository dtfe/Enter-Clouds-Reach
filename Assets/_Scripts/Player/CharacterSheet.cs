using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField]
    TMP_Text test;
    [SerializeField]
    TMP_Text[] statText;
    void Start()
    {
        PlayerTraits[] traits = playerStats.traitArray;
        for(int i = 0; i < statText.Length; i++)
        {
            if(playerStats.Stats.ContainsKey(statText[i].name)){
                statText[i].SetText(playerStats.Stats[statText[i].name].ToString());
            }
            else Debug.Log(playerStats.Stats.Keys.ToString());
            Debug.Log(statText[i]);
        }
        int j = 0;
        int a = 3;
        
        foreach(PlayerTraits playerTrait in traits)
        {
            if(traits[j].traitBool)
            {
            TMP_Text text = Instantiate(test,gameObject.transform, false);
            TMP_Text desc = Instantiate(test,gameObject.transform, false);
            desc.SetText("");
            text.SetText("");
            text.transform.localPosition = text.transform.localPosition + new Vector3 (100*a,0,0);
            desc.transform.localPosition = desc.transform.localPosition + new Vector3(100*a,-100,0);
            text.SetText(traits[j].traitName);
            desc.SetText(traits[j].traitDesc);
            Debug.Log(traits[j].traitBool);
            a +=3;
            }
            j++;
            Debug.Log(traits);
            Debug.Log(traits.Length);
        }
    
       
}
}
