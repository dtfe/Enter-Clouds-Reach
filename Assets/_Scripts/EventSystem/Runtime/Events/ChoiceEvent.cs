using EnterCloudsReach.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    public class ChoiceEvent : EventClass
    {

        public override void StartEvent()
        {
            StartChoice();
        }

        public override void StartChoice()
        {
            string[] commands = new string[eventChoices.Length];
            for (int i = 0; i < eventChoices.Length; i++)
            {
                commands[i] = eventChoices[i].eventName;
            }

            GUI_Manager.DialogueBox.QueUpText(eventText, commands, SelectNextEvent);
        }
    }
}
