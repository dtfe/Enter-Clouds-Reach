using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnterCloudsReach.EventSystem;

namespace EnterCloudsReach.GUI
{
    public class GUI_DialogueEvent : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private TMP_Text eventText;
        
        private int index;

        public void Initialize(string EventName, int Index)
        {
            eventText.text = EventName;
            index = Index;
        }

        public void SelectEvent()
        {
            if(GUI_Manager.DialogueBox.rollEvent != null)
            {
                if(GUI_Manager.DialogueBox.rollEvent.GetComponent<RollEvent>() != null && GUI_Manager.DialogueBox.indexRoll == index)
                {
                    GUI_Manager.DialogueBox.RollPopUP.SetActive(true);
                    GUI_Manager.DialogueBox.RollPopUP.GetComponent<RollEventPopUp>().SetRollEvent(GUI_Manager.DialogueBox.rollEvent, index);
                    Debug.Log("hi");
                    return;
                }
            }
            GUI_Manager.DialogueBox.eventReturnIndex = index;
            GUI_Manager.DialogueBox.rollEvent = null;
            
        }
    }
}
