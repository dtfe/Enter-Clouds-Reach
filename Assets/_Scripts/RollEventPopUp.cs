using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.GUI;
using TMPro;

namespace EnterCloudsReach.EventSystem
{
    public class RollEventPopUp : MonoBehaviour
    {
        private EventClass rollE;
        int index;
        public TMP_Text rollInfo;
        int rollN;
        RollEvent.StatName statN;
        public void AcceptRoll()
        {
            GUI_Manager.DialogueBox.eventReturnIndex = index;
            for(int i = 0; i < GUI_Manager.DialogueBox.rollEvent.Length; i++) 
            {
                GUI_Manager.DialogueBox.rollEvent[i] = null;
            } 
            gameObject.SetActive(false);
        }
        public void DeclineRoll()
        {
            gameObject.SetActive(false);
        }
        public void SetRollEvent(EventClass e,int i) 
        {
            rollE = e;
            index = i;
            statN = rollE.GetComponent<RollEvent>().statName;
            string StatToRoll = "";
            switch (statN)
            {
                default:
                    StatToRoll = "Missing Stat";
                    break;

                case RollEvent.StatName.Brawn:
                    StatToRoll = "Brawn";
                    break;

                case RollEvent.StatName.Agility:
                    StatToRoll = "Agility";
                    break;

                case RollEvent.StatName.Endurance:
                    StatToRoll = "Endurance";
                    break;

                case RollEvent.StatName.Knowledge:
                    StatToRoll = "Knowledge";
                    break;

                case RollEvent.StatName.Wisdom:
                    StatToRoll = "Wisdom";
                    break;

                case RollEvent.StatName.Charm:
                    StatToRoll = "Charm";
                    break;
            }
            rollN= rollE.GetComponent<RollEvent>().rollCheck;
            string infoText = $"Your dice give you a vision of your chances: You need to beat a {rollN} {StatToRoll} check to succeed this action";
            rollInfo.SetText(infoText);
        }
    }
}
