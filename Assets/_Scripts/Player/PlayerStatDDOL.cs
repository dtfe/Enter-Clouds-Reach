using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Player
{
    public class PlayerStatDDOL : MonoBehaviour
    {
        public PlayerStats playerStats;
        private void OnEnable()
        {
            playerStats.health = playerStats.maxHealth;
        }
    }
}
