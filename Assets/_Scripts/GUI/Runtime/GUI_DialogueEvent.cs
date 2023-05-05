using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnterCloudsReach.EventSystem;
using System.Linq;

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
            if(GUI_Manager.DialogueBox.rollEvent[index] != null)
            {
                if(GUI_Manager.DialogueBox.rollEvent[index].GetComponent<RollEvent>() != null)
                {
                    GUI_Manager.DialogueBox.RollPopUP.SetActive(true);
                    GUI_Manager.DialogueBox.RollPopUP.GetComponent<RollEventPopUp>().SetRollEvent(GUI_Manager.DialogueBox.rollEvent[index], index);
                    Debug.Log("hi");
                    return;
                }
            }
            for(int i = 0; i < GUI_Manager.DialogueBox.rollEvent.Length; i++) 
            {
                GUI_Manager.DialogueBox.rollEvent[i] = null;
            } 
            GUI_Manager.DialogueBox.eventReturnIndex = index;
            
            
        }
    }
}
