using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    public TMP_Text nameText;
    public Slider hpSlider;

    public GameObject statusEffects;

    private GameObject stunned;
    private TMP_Text StunnedText;
        
    private GameObject bleed;
    private TMP_Text bleedText;
        
    private GameObject poison;
    private TMP_Text poisonText;


    private void Start()
    {
        nameText = transform.Find("Name").GetComponent<TMP_Text>();
        hpSlider = transform.Find("Health").GetComponent<Slider>();
        statusEffects = transform.Find("Status Effects").gameObject;
        #region status Effect Params
        stunned = statusEffects.transform.Find("Confusion").gameObject;
        StunnedText = stunned.GetComponentInChildren<TMP_Text>();

        bleed = statusEffects.transform.Find("Bleed").gameObject;
        bleedText = bleed.GetComponentInChildren<TMP_Text>();

        poison = statusEffects.transform.Find("Poison").gameObject;
        poisonText = poison.GetComponentInChildren<TMP_Text>();
        #endregion status Effect Params
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.minValue = 0;
        hpSlider.value = unit.curHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void refreshStatus(Unit unit)
    {
        StunnedText.text = unit.stunned.ToString();
        poisonText.text = unit.poison.ToString();
        bleedText.text = unit.bleed.ToString();
        if (unit.stunned != 0)
        {
            stunned.SetActive(true);
        }
        else
        {
            stunned.SetActive(false);
        }

        if (unit.bleed != 0)
        {
            bleed.SetActive(true);
            transform.parent.Find("Body").Find("Blood").gameObject.SetActive(true);
        }
        else
        {
            bleed.SetActive(false);
            transform.parent.Find("Body").Find("Blood").gameObject.SetActive(false);
        }

        if (unit.poison != 0)
        {
            poison.SetActive(true);
        }
        else
        {
            poison.SetActive(false);
        }
    }
}
