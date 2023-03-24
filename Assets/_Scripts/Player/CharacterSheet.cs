using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSheet : MonoBehaviour
{
    float time = 0;
    public PlayerStats playerStats;
    bool isMove;
    [SerializeField]
    TMP_Text traitText;
    [SerializeField]
    TMP_Text[] statText;   
    private RectTransform c_RectTransform;
    public Vector2 prePos;
    public Vector2 startPos;
    public Vector2 tarPos;
    ButtonSFX buttonSFX;
    void Start()
    {
        buttonSFX = gameObject.GetComponent<ButtonSFX>();
        c_RectTransform = GetComponent<RectTransform>();
        c_RectTransform.localPosition = c_RectTransform.localPosition + new Vector3(0,500,0);
        startPos = c_RectTransform.anchoredPosition;
        for(int i = 0; i < statText.Length; i++)
        {
            if(playerStats.Stats.ContainsKey(statText[i].name)){
                statText[i].SetText(playerStats.Stats[statText[i].name].ToString());
            }
            else Debug.Log(playerStats.Stats.Keys.ToString());
            Debug.Log(statText[i]);
        }
        int j = 0;
        int a = 1;
        Trait[] traits = playerStats.traits;
        if(traits != null){
        foreach(Trait t in traits)
        {
            if(traits[j].playerTraits.traitBool)
            {
            TMP_Text text = Instantiate(traitText,gameObject.transform, false);
            text.name = traits[j].playerTraits.traitName;
            text.SetText("");
            text.transform.localPosition = text.transform.localPosition + new Vector3 (100*a,0,0);
            text.SetText(traits[j].playerTraits.traitName);
            Debug.Log(traits[j].playerTraits.traitBool);
            a += 2;
            }
            j++;
            Debug.Log(traits);
            Debug.Log(traits.Length);
        }
        }
    }
    void Update()
    {
        if (!isMove)
        {
            prePos = c_RectTransform.anchoredPosition;
        }
        else
        {
            time += Time.deltaTime * 0.9f;
            Vector2 moveUp = Vector2.Lerp(prePos, tarPos, time);
            c_RectTransform.anchoredPosition = moveUp;
            if (c_RectTransform.anchoredPosition == tarPos)
            {
                isMove = false;
            }
        }
    }
    public void MoveSheet()
    {
        if(buttonSFX != null)
        {
            buttonSFX.ButtonSFXPlay();
        }
        if(prePos.y == startPos.y)
        {
        time = 0;
        tarPos = new Vector2(prePos.x,prePos.y-500);
        isMove = true;
        }
        else if(prePos.y != startPos.y)
        {
        time = 0;
        tarPos = new Vector2(prePos.x,prePos.y+500);
        isMove = true;
        }
    }
}
