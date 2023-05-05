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
            for(int i = 0; i < GUI_Manager.DialogueBox.rollEvent.Length; i++) 
            {
                GUI_Manager.DialogueBox.rollEvent[i] = null;
            } 
            for (int i = 0; i < eventChoices.Length; i++)
            {
                commands[i] = eventChoices[i].eventName;
                if(eventChoices[i].GetComponent<RollEvent>() != null)
                {
                    GUI_Manager.DialogueBox.RollEvent(eventChoices[i], i);
                }
            }

            GUI_Manager.DialogueBox.QueUpText(eventText, commands, EndEvent);
        }
    }
}
