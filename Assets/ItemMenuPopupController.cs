using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnterCloudsReach.Inventory
{
    public class ItemMenuPopupController : MonoBehaviour
    {
        public GameObject parent;

        [Header("Examine Prefabs")]
        public GameObject weaponExamine;
        public GameObject notesExamine;
        public GameObject consumableExamine;
        public GameObject armorExamine;


        private Item itemRef;
        private Animator animSelf;

        private ItemUIController parentController;
        private EquipmentSlot parentSlot;

        private void Start()
        {
            animSelf = GetComponent<Animator>();
        }

        private void Update()
        {
            if (parentController != null && !parentController.GetIsOver || parentSlot != null && !parentSlot.isOver)
            {
                Deselected();
            }
        }

        public Item setItem
        {
            get { return itemRef; }
            set { itemRef = value; }
        }

        public EquipmentSlot setParentSlot
        {
            set 
            {
                parentSlot = value;
            }
        }

        public ItemUIController SetItemUiController
        {
            set { parentController = value; }

        }

        public void ExamineItem()
        {
            GameObject spawnedExamine = null;

            switch (itemRef.itemClass)
            {

                default:
                    break;

                case itemType.Weapon:
                    spawnedExamine = Instantiate(weaponExamine, FindObjectOfType<InventoryManager>().transform);
                    break;

                case itemType.Note:
                    spawnedExamine = Instantiate(notesExamine, FindObjectOfType<InventoryManager>().transform);
                    break;

                case itemType.Consumable:
                    spawnedExamine = Instantiate(consumableExamine, FindObjectOfType<InventoryManager>().transform);
                    break;

                case itemType.Armor:
                    spawnedExamine = Instantiate(armorExamine, FindObjectOfType<InventoryManager>().transform);
                    break;
            }
            if (spawnedExamine != null)
            {
                spawnedExamine.GetComponent<InvExamineController>().referencedItem = itemRef;
            }
            Deselected();
        }

        public void DropItem()
        {
            Deselected();
            FindObjectOfType<InventoryManager>().RemoveItem(parentController.itemReference);
        }

        public void Deselected()
        {
            foreach(Button btn in GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            animSelf.SetTrigger("Exit");
        }

        public void DestroyMenu()
        {
            Destroy(parent);
        }

        public void UnequipItem()
        {
            Deselected();
            parentSlot.UnqeuipItem();
        }
    }
}
