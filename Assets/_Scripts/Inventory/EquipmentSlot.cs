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
        
        private Image startIcon;
        private Color startColor;
        private Sprite startIconSprite;

        private EquipmentSlotManager manager;

        public bool isOver;

        private void Start()
        {
            startIcon = GetComponent<Image>();
            startIconSprite = startIcon.sprite;
            startColor = startIcon.color;
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
            if(equipmentItem != null)
            {
                icon.sprite = equipmentItem.icon;
                icon.color = Color.white;
            }
            else
            {
                icon.sprite = startIconSprite;
                icon.color = startColor;
            }
        }

        public void UnqeuipItem()
        {
            GetComponentInParent<InventoryManager>().AddItem(equipmentItem);
            equipmentItem = null;
            updateSlot();
        }
    }

}
