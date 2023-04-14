using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnterCloudsReach.Player;

public class AbilityStatUI : MonoBehaviour
{
    public string Stat;
    private TMP_Text Bonus;
    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStatDDOL>().playerStats;
        Debug.Log(stats.health);
        Debug.Log(stats.ModStats.Values.Count);
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        foreach(TMP_Text text in texts){
            if(text.name == "BonusText")
            {
                Bonus = text;
            }
        }
        if(stats.ModStats.Values.Count != 0){
        UpdateStat();
        UpdateBonus();
    }}
    public void UpdateStat() => GetComponentInChildren<TMP_Text>().text = stats.ModStats[Stat].ToString();
    public void UpdateBonus()
    {
        int bonus = stats.GetBonus(Stat);
        if(bonus <= 0)
        {
            Bonus.SetText(bonus.ToString());
        }
        else
        {
            Bonus.SetText("+"+bonus.ToString());
        }
    }
}
