using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityStatUI : MonoBehaviour
{
    public string Stat;
    private PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = Resources.Load<PlayerStats>("PlayerStatsObject");

        GetComponentInChildren<TMP_Text>().text = stats.GetStat(Stat).ToString();
    }
}
