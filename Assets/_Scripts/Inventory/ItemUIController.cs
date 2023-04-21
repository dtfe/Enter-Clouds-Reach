using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnterCloudsReach.Inventory
{
    public class ItemUIController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Color draggingColor;

        public Item itemReference;

        public GameObject menuPopup;
        private GameObject spawnedPopup;

        private GameObject draggedItem;
        private Rect selfRect;

        private bool isOver;

        private EquipmentSlotManager equipmentManager;


        private void Start()
        {
            selfRect = GetComponent<RectTransform>().rect;

            equipmentManager = FindObjectOfType<EquipmentSlotManager>();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("StartingDrag");
            equipmentManager.isDragging = true;
            if (!draggedItem)
            {
                SpawnGhostItem();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            draggedItem.transform.position = mousePos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("EndDrag");
            if (draggedItem)
            {
                if (equipmentManager.overSlot && itemReference.equipmentSlot == equipmentManager.overSlot.typeOfSlot)
                {
                    equipmentManager.equipToSlot();
                    Destroy(draggedItem);
                    equipmentManager.draggedItem = null;
                    equipmentManager.isDragging = false;
                    InventoryManager inventoryManager = GetComponentInParent<InventoryManager>();
                    inventoryManager.RemoveItem(itemReference);
                    inventoryManager.ListItems();
                }
                else
                {
                    Destroy(draggedItem);
                    equipmentManager.draggedItem = null;
                }
            }
            equipmentManager.isDragging = false;
        }


        private void SpawnGhostItem()
        {
            equipmentManager.draggedItem = itemReference;
            draggedItem = Instantiate(gameObject, GetComponentInParent<InventoryController>().transform);
            RectTransform draggedItemRT = draggedItem.GetComponent<RectTransform>();
            draggedItemRT.sizeDelta = selfRect.size;
            draggedItem.GetComponent<Image>().raycastTarget = false;
            Destroy(draggedItem.GetComponent<ItemUIController>());
            draggedItem.GetComponent<Image>().color = draggingColor;
            foreach(Image image in draggedItem.GetComponentsInChildren<Image>())
            {
                image.raycastTarget = false;
                image.color = draggingColor;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("MouseIsOverItem");
            isOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("MouseIsOffItem");
            isOver = false;
        }


        public bool GetIsOver
        {

            get { return isOver; }
        }

        public void SpawnMenuPopup()
        {
            if (spawnedPopup)
            {
                return;
            }
            spawnedPopup = Instantiate(menuPopup, transform);
            spawnedPopup.GetComponentInChildren<ItemMenuPopupController>().SetItemUiController = this;
            spawnedPopup.GetComponentInChildren<ItemMenuPopupController>().setItem = itemReference;
        }
    }
}
