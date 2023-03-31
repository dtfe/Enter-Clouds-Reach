using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Inventory
{
    public enum slotType
    {
        NONE,
        HEAD,
        CHEST,
        HAND,
        FEET
    }

    public class EquipmentSlotManager : MonoBehaviour
    {
        public bool isDragging;

        public Item draggedItem;

        public EquipmentSlot overSlot;

        public void equipToSlot()
        {
            overSlot.equipItem(draggedItem);
        }

    }
}
