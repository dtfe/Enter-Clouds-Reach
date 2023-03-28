using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.EventSystem
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class EventAttribute : System.Attribute
    {
        public bool enableText { get; private set; }
        public byte minEvents { get; private set; }
        public byte maxEvents { get; private set; }

        private EventAttribute()
        {
            throw new System.NotImplementedException();
        }

        public EventAttribute(bool EnableText, byte MinEvents, byte MaxEvents)
        {
            enableText = EnableText;
            minEvents = MinEvents;
            maxEvents = MaxEvents;
        }
    }
}
