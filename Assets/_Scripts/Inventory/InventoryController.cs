using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Inventory
{

    public class InventoryController : MonoBehaviour
    {
        private Animator self;

        private void Start()
        {
            self = GetComponent<Animator>();
        }

        private void Update()
        {
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
            }
        }

        public void OpenInventory()
        {
            GetComponent<InventoryManager>().ListItems();
            self.SetBool("OpenInventory", true);
        }

        public void CloseInventory()
        {
            self.SetBool("OpenInventory", false);
        }
    }
}
