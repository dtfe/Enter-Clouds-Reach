using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private Animator animSelf;

        private ItemUIController parentController;

        private void Start()
        {
            animSelf = GetComponent<Animator>();
        }

        private void Update()
        {
            if (parentController != null && !parentController.GetIsOver)
            {
                Deselected();
            }
        }

        public ItemUIController SetItemUiController
        {
            set { parentController = value; }

        }

        public void ExamineItem()
        {
            GameObject spawnedExamine = null;
            switch (parentController.itemReference.itemClass)
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
                spawnedExamine.GetComponent<InvExamineController>().referencedItem = parentController.itemReference;
            }
            Deselected();
        }

        public void DropItem()
        {
            FindObjectOfType<InventoryManager>().RemoveItem(parentController.itemReference);
        }

        public void Deselected()
        {
            animSelf.SetTrigger("Exit");
        }

        public void DestroyMenu()
        {
            Destroy(parent);
        }
    }
}
