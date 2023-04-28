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
        public List<Item> eqWeapons = new List<Item>();

        public void equipToSlot()
        {
            overSlot.equipItem(draggedItem);
        }

        public void SetWeapon(Item item)
        {
            if(item.itemClass == itemType.Weapon)
            {
                eqWeapons.Add(item);
            }
        }
        public void RemoveWeapon(Item item)
        {
            if(item.itemClass == itemType.Weapon && eqWeapons.Contains(item))
            {
                int i = eqWeapons.IndexOf(item);
                eqWeapons.RemoveAt(i);
            }
        }

    }
}
