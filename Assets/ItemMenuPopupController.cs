using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Inventory
{
    public class ItemMenuPopupController : MonoBehaviour
    {
        public GameObject parent;

        private Animator animSelf;

        private ItemUIController parentController;

        private void Start()
        {
            animSelf = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!parentController.GetIsOver)
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
