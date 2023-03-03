using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class TraitTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerStats playerStats;
    PlayerTraits trait;
    public TMP_Text text;
    TMP_Text sText;
    bool spawnText = true;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {   
        if(spawnText){
            trait = playerStats.GetPlayerTrait(gameObject.name);
            sText= Instantiate(text,gameObject.transform);
            sText.text = trait.traitDesc;
            spawnText=false;
            }
        sText.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        sText.gameObject.SetActive(false);
    }    
}
