using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Player
{
    public class ModStatsInit : MonoBehaviour
    {
        PlayerStats playerStats;
        private void Start() 
        {
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;    
        }
        public void SetModStatsInit()
        {
            if(playerStats.ModStats.Values.Count == 0)
            {
            playerStats.ModStats = playerStats.BaseStats;
            Debug.Log(playerStats.ModStats.Values.Count);
            }
            else
            {
                playerStats.ModStats.Clear();
                playerStats.ModStats = playerStats.BaseStats;
            }
        }
    }
}