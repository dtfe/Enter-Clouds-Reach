using System.Collections;
using System.Collections.Generic;
using EnterCloudsReach.GUI;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    public class TextEvent : EventClass
    {
        public override void StartEvent()
        {
            StartChoice();
        }

        public override void StartChoice()
        {
            GUI_Manager.DialogueBox.QueUpText(eventText, new string[0], SelectNextEvent);
        }
    }
}
