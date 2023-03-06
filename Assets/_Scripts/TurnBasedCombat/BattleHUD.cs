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
    public GameObject bloodPS;
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
        bleed.GetComponent<Image>().sprite = Resources.Load<Sprite>("bloodIcon.png");
        bloodPS = transform.parent.Find("Body").Find("Blood").gameObject;


        poison = statusEffects.transform.Find("Poison").gameObject;
        poisonText = poison.GetComponentInChildren<TMP_Text>();
        bleed.GetComponent<Image>().sprite = Resources.Load<Sprite>("poisonIcon.png");
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
        if (hp <= 0)
        {
            hpSlider.transform.Find("Fill Area").gameObject.SetActive(false);
        }
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
            bloodPS.SetActive(true);
        }
        else
        {
            bleed.SetActive(false);
            bloodPS.SetActive(false);
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
