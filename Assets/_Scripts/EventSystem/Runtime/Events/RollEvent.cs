using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    [Event(false, 2, 2)]
    public class RollEvent : EventClass
    {
        
        [SerializeField] private int rollCheck = 4;
        [SerializeField] private DieRoller.dFaces faceType = DieRoller.dFaces.d6;
        [SerializeField] private AbilityCheckScript.Ability ability = AbilityCheckScript.Ability.None;
        public override void StartEvent()
        {
            
            DieRoller.RollDie(faceType, RollCheck);
        }

        public void RollCheck(int Roll)
        {
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
    }
}
