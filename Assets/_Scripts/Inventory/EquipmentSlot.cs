using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EnterCloudsReach.Inventory
{
    public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public slotType typeOfSlot;

        private Item equipmentItem;

        private Image icon;

        private Sprite iconSprite;

        private EquipmentSlotManager manager;

        public bool isOver;

        private void Start()
        {
            manager = GetComponentInParent<EquipmentSlotManager>();
            icon = transform.Find("Icon").GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Mouse Entered: " + gameObject.name);
            isOver = true;
            manager.overSlot = this;
        }

        

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Mouse Exited: " + gameObject.name);
            isOver = false;
            manager.overSlot = null;
        }

        public void equipItem(Item item)
        {
            equipmentItem = item;
            updateSlot();
        }

        private void updateSlot()
        {
            icon.sprite = equipmentItem.icon;
            icon.color = Color.white;
        }
    }
}
