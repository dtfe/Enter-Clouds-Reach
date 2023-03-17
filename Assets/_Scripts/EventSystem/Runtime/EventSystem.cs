using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    public class EventSystem : MonoBehaviour
    {
        public EventClass startingEvent = null;

        public void Start()
        {
            startingEvent.StartEvent();
        }
    }
}
