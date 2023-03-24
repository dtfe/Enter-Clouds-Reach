using System.Collections;
using System.Collections.Generic;
using EnterCloudsReach.GUI;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    [Event(true, 1)]
    public class TextEvent : EventClass
    {
        public override void StartEvent()
        {
            GUI_Manager.DialogueBox.QueUpText(eventText, new string[0], EndEvent);
        }
    }
}
