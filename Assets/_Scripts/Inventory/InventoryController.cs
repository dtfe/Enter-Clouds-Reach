using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EnterCloudsReach.Inventory
{

    public class InventoryController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public Button openInv;
        private bool isOver;

        private Animator self;
        private InventoryManager invManager;

        private void Start()
        {
            self = GetComponent<Animator>();
            invManager = GetComponent<InventoryManager>();
        }

        private void Update()
        {
            if (!isOver)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CloseInventory();
                }
            }

            /*
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(self.GetBool("OpenInventory") == false)
                {
                    OpenInventory();
                }
                else
                {
                    CloseInventory();
                }
            }*/
        }

        public void OpenInventory()
        {
            self.SetBool("OpenInventory", true);
        }

        public void CloseInventory()
        {
            self.SetBool("OpenInventory", false);
        }

        public void EnableButton()
        {
            openInv.interactable = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isOver = false;
        }
    }
}
