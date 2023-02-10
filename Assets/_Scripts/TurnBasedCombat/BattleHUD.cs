using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{

    public TMP_Text nameText;
    public Slider hpSlider;

    private void Start()
    {
        nameText = transform.Find("Name").GetComponent<TMP_Text>();
        hpSlider = transform.Find("Health").GetComponent<Slider>();
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
}
