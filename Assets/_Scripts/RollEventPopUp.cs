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
        public void AcceptRoll()
        {
            GUI_Manager.DialogueBox.eventReturnIndex = index;
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
            rollN= rollE.GetComponent<RollEvent>().rollCheck;
            string infoText = $"You need a {rollN} or higher. You can choose to test your luck or check if a different path";
            rollInfo.SetText(infoText);
        }
    }
}
