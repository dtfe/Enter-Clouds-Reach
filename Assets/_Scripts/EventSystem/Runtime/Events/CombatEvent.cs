using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.GUI;
using EnterCloudsReach.Combat;

namespace EnterCloudsReach.EventSystem
{
    [Event(false,1,1)]
    public class CombatEvent : EventClass
    {
        public GameObject enemyPrefab;

        [SerializeField] private BattleType typeOfBattle;

        [Tooltip("Will activate this bool in playerprefs")]
        [SerializeField] private string BoolToEnable;

        BattleLoader battleLoader;
        public override void StartEvent()
        {   
            battleLoader = FindObjectOfType<BattleLoader>();
            
            battleLoader.StartBattleNew(enemyPrefab,eventChoices[0], typeOfBattle);
            if(BoolToEnable != "")
            {
                PlayerPrefs.SetInt(BoolToEnable, 1);
            }
        }
    }
}
