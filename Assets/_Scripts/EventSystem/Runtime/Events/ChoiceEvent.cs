using EnterCloudsReach.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    [Event(true, 2, 5)]
    public class ChoiceEvent : EventClass
    {
        public override void StartEvent()
        {
            string[] commands = new string[eventChoices.Length];
            for (int i = 0; i < eventChoices.Length; i++)
            {
                commands[i] = eventChoices[i].eventName;
            }

            GUI_Manager.DialogueBox.QueUpText(eventText, commands, EndEvent);
        }
    }
}