using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    public abstract class EventClass : MonoBehaviour
    {
        [HideInInspector] public string eventName = "Name";
        [HideInInspector] public string[] eventText = { "This is an Event!", "It can have multiple lines!" };
        [HideInInspector] public EventClass[] eventChoices = null;

        public abstract void StartEvent();
        public abstract void StartChoice();

        public void SelectNextEvent(int NextEventID)
        {
            SelectNextEvent(eventChoices[NextEventID]);
        }

        public void SelectNextEvent(EventClass NextEvent)
        {
            NextEvent.StartEvent();
        }
    }
}
