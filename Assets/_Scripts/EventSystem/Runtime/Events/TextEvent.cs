using System.Collections;
using System.Collections.Generic;
using EnterCloudsReach.GUI;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    [Event(true, 1, 1)]
    public class TextEvent : EventClass
    {
        [Tooltip("Will activate this bool in playerprefs")]
        [SerializeField] private string BoolToEnable;

        public override void StartEvent()
        {
            GUI_Manager.DialogueBox.QueUpText(eventText, new string[0], EndEvent);
            if(BoolToEnable != "")
            {
                PlayerPrefs.SetInt(BoolToEnable, 1);
            }
        }
    }
}
