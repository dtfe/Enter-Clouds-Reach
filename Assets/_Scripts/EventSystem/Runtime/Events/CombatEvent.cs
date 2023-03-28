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
        BattleLoader battleLoader;
        public override void StartEvent()
        {   
            battleLoader = FindObjectOfType<BattleLoader>();
            
            battleLoader.StartBattleNew(enemyPrefab,eventChoices[0]);
        }
    }
}
