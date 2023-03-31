using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnterCloudsReach.Player;

public class AbilityStatUI : MonoBehaviour
{
    public string Stat;
    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<PlayerStatDDOL>().playerStats;
        Debug.Log(stats.health);
        Debug.Log(stats.Stats.Values.Count);
        if(stats.Stats.Values.Count != 0){
        UpdateStat();
    }}
    public void UpdateStat() => GetComponentInChildren<TMP_Text>().text = stats.GetStat(Stat).ToString();
}
