using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.Player;
using TMPro;

namespace EnterCloudsReach.Inventory
{
    public class InventoryPlayerName : MonoBehaviour
    {
        private PlayerStats playerStats;
        private TMP_Text playerName;

        void Start()
        {
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
            playerName = gameObject.GetComponentInChildren<TMP_Text>();
            playerName.SetText(playerStats.playerName);

        }
    }
}
