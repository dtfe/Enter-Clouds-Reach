using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            GUI_Manager.DialogueBox.eventReturnIndex = index;
        }
    }
}
