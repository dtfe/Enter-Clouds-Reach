using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace EnterCloudsReach.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        public List<Item> itemList = new List<Item>();

        public Transform itemContent;
        public GameObject inventoryItem;


        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
        }

        public void ListItems()
        {
            //Cleans up items before opening inventory
            foreach(Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }

            //Adds items to inventory
            foreach(Item item in itemList)
            {
                GameObject obj = Instantiate(inventoryItem, itemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
            }
        }
    }
}