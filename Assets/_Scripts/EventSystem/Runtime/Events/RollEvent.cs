using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.Player;

namespace EnterCloudsReach.EventSystem
{
    [Event(false, 2, 2)]
    public class RollEvent : EventClass
    {
        public enum StatName{None,Brawn,Agility,Endurance,Knowledge,Wisdom,Charm}
        [SerializeField] internal int rollCheck = 4;
        [SerializeField] private DieRoller.dFaces faceType = DieRoller.dFaces.d6;
        [SerializeField] internal StatName statName = StatName.None;
        private PlayerStats playerStats;
       
        private void Start()
        {
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
        }
        public override void StartEvent()
        {
            
            DieRoller.RollDie(faceType, RollCheck);
        }

        public void RollCheck(int Roll)
        {   Roll = ModifyRoll(Roll);
            if (Roll < rollCheck)
            {
                // Fail
                EndEvent(0);
            }
            else
            {
                // Pass
                EndEvent(1);
            }
        }
        private int mod;
        int ModifyRoll(int modRoll)
        {
            
            if(playerStats.ModStats.Count != 0)
            {
            switch(statName)
            {   
                case StatName.Brawn:
                    mod = playerStats.GetBonus("Brawn");
                    break;
                case StatName.Agility:
                    mod = playerStats.GetBonus("Agility");
                    break;
                case StatName.Endurance:
                    mod = playerStats.GetBonus("Endurance");
                    break;
                case StatName.Knowledge:
                    mod = playerStats.GetBonus("Knowledge");
                    break;
                case StatName.Wisdom:
                    mod = playerStats.GetBonus("Wisdom");
                    break;
                case StatName.Charm:
                    mod = playerStats.GetBonus("Charm");
                    break;
                default:
                    break;
            }
            }
            modRoll += mod;
            return modRoll;
        }
    }
}
