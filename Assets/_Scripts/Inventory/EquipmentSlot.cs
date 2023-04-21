using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using EnterCloudsReach.Player;

namespace EnterCloudsReach.Inventory
{
    public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        // Bonus Overview Stuff
        public GameObject bonusOverview;
        public GameObject bonusOverviewInfo;
        private GameObject curBonusUI;

        // Equipment Actions
        public GameObject equipmentActions;

        public slotType typeOfSlot;

        private Item equipmentItem;
        private PlayerStats playerStats;

        private Image icon;
        
        private Image selfIcon;
        private Color startColor;
        private Sprite startIconSprite;

        private EquipmentSlotManager manager;

        public bool isOver;
        AbilityStatUI[] abi;

        private void Start()
        {
            selfIcon = GetComponent<Image>();
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
            manager = GetComponentInParent<EquipmentSlotManager>();
            icon = transform.Find("Icon").GetComponent<Image>();
            startIconSprite = icon.sprite;
            startColor = icon.color;
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
        {   
            if(equipmentItem != null && equipmentItem.affectStats)
            {   
                ItemStatNeg(equipmentItem);
                if (curBonusUI)
                {
                    Destroy(curBonusUI);
                }
            }
            equipmentItem = item;
            if(item.affectStats){
                ItemStatPos(item);
                curBonusUI = Instantiate(bonusOverviewInfo, bonusOverview.transform);
                curBonusUI.GetComponent<BonusOverviewItemController>().ApplyItem(equipmentItem);
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
            if (equipmentItem != null && equipmentItem.affectStats)
            {
                ItemStatNeg(equipmentItem);
                if (curBonusUI)
                {
                    Destroy(curBonusUI);
                }
            }
            GetComponentInParent<InventoryManager>().AddItem(equipmentItem);
            equipmentItem = null;
                        updateSlot();
        }
        
        void ItemStatPos(Item item)
        {
            if(playerStats.ModStats.Values.Count != 0){
            playerStats.ModStats["Brawn"] += item.Brawn;
            playerStats.ModStats["Agility"] += item.Agility;
            playerStats.ModStats["Endurance"] += item.Endurance;
            playerStats.ModStats["Knowledge"] += item.Knowledge;
            playerStats.ModStats["Wisdom"] += item.Wisdom;
            playerStats.ModStats["Charm"] += item.Charm; 
            foreach(var obj in abi) {
                obj.UpdateStat();
                obj.UpdateBonus();
            }
            }
        }   
         void ItemStatNeg(Item item)
        {
            if(playerStats.ModStats.Values.Count != 0){
            playerStats.ModStats["Brawn"] -= item.Brawn;
            playerStats.ModStats["Agility"] -= item.Agility;
            playerStats.ModStats["Endurance"] -= item.Endurance;
            playerStats.ModStats["Knowledge"] -= item.Knowledge;
            playerStats.ModStats["Wisdom"] -= item.Wisdom;
            playerStats.ModStats["Charm"] -= item.Charm; 
            foreach(var obj in abi) 
                {
                    obj.UpdateStat();
                    obj.UpdateBonus();
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(equipmentItem != null)
            {
                GameObject go = Instantiate(equipmentActions, transform);
                ItemMenuPopupController eaController = go.GetComponentInChildren<ItemMenuPopupController>();
                eaController.setParentSlot = this;
                eaController.setItem = equipmentItem;
            }
        }
    }

}
