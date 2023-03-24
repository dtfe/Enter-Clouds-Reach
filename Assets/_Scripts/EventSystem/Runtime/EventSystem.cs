using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    public class EventSystem : MonoBehaviour
    {
        public EventClass startingEvent = null;

        public IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            startingEvent.StartEvent();
        }
    }
}
