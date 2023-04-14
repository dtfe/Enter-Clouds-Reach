using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EnterCloudsReach.Player;

namespace EnterCloudsReach.Inventory
{
    public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public slotType typeOfSlot;

        private Item equipmentItem;
        private PlayerStats playerStats;

        private Image icon;
        
        private Image startIcon;
        private Color startColor;
        private Sprite startIconSprite;

        private EquipmentSlotManager manager;

        public bool isOver;
        AbilityStatUI[] abi;

        private void Start()
        {
            startIcon = GetComponent<Image>();
            startIconSprite = startIcon.sprite;
            startColor = startIcon.color;
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
            manager = GetComponentInParent<EquipmentSlotManager>();
            icon = transform.Find("Icon").GetComponent<Image>();
            abi = FindObjectsOfType<AbilityStatUI>();
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
        {   if(equipmentItem != null && equipmentItem.affectStats)
            {   
                ItemStatNeg(equipmentItem);
            }
            equipmentItem = item;
            if(item.affectStats){
               ItemStatPos(item);
            }
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
        
        void ItemStatPos(Item item)
        {
            if(playerStats.Stats.Values.Count != 0){
            playerStats.Stats["Brawn"] += item.Brawn;
            playerStats.Stats["Agility"] += item.Agility;
            playerStats.Stats["Endurance"] += item.Endurance;
            playerStats.Stats["Knowledge"] += item.Knowledge;
            playerStats.Stats["Wisdom"] += item.Wisdom;
            playerStats.Stats["Charm"] += item.Charm; 
            foreach(var obj in abi) {
                obj.UpdateStat();
                obj.UpdateBonus();
            }
            }
        }   
         void ItemStatNeg(Item item)
        {
            if(playerStats.Stats.Values.Count != 0){
            playerStats.Stats["Brawn"] -= item.Brawn;
            playerStats.Stats["Agility"] -= item.Agility;
            playerStats.Stats["Endurance"] -= item.Endurance;
            playerStats.Stats["Knowledge"] -= item.Knowledge;
            playerStats.Stats["Wisdom"] -= item.Wisdom;
            playerStats.Stats["Charm"] -= item.Charm; 
            foreach(var obj in abi) {
                obj.UpdateStat();
                obj.UpdateBonus();
            }
            }
        }   
    }

}
