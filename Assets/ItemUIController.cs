using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnterCloudsReach.Inventory
{
    public class ItemUIController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Color draggingColor;

        private GameObject draggedItem;
        private Rect selfRect;


        private void Start()
        {
            selfRect = GetComponent<RectTransform>().rect;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("StartingDrag");
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
                Destroy(draggedItem);
            }
        }


        private void SpawnGhostItem()
        {
            draggedItem = Instantiate(gameObject, GetComponentInParent<InventoryController>().transform);
            RectTransform draggedItemRT = draggedItem.GetComponent<RectTransform>();
            draggedItemRT.sizeDelta = selfRect.size;
            Destroy(draggedItem.GetComponent<ItemUIController>());
            draggedItem.GetComponent<Image>().color = draggingColor;
            foreach(Image image in draggedItem.GetComponentsInChildren<Image>())
            {
                image.color = draggingColor;
            }
        }


    }
}
