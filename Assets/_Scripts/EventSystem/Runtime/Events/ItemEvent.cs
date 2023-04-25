using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.Inventory;
using EnterCloudsReach.GUI;

namespace EnterCloudsReach.EventSystem
{
    [Event(true, 1, 1)]
    public class ItemEvent : EventClass
    {
        [SerializeField] private Item item;
        private InventoryManager inv;
        private void Start()
        {
            if(item == null)
            {
                Debug.LogWarning("Set Item please at " + gameObject.name);
            }
            inv = FindObjectOfType<InventoryManager>();
        }
        public override void StartEvent()
        {
            inv.AddItem(item);
            GUI_Manager.DialogueBox.QueUpText(eventText,new string[0],EndEvent);
        }
    }
}
