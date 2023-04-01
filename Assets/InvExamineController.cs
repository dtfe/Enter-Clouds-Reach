using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace EnterCloudsReach.Inventory
{
    public class InvExamineController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Item referencedItem;

        private bool isOver;

        private void Start()
        {
            switch (referencedItem.itemClass)
            {
                default:
                    Debug.LogWarning("How.");
                    break;

                case itemType.Weapon:

                    break;

                case itemType.Note:
                    transform.Find("Text").GetComponent<TMP_Text>().text = referencedItem.notes;
                    break;

                case itemType.Consumable:

                    break;

                case itemType.Armor:
                    transform.Find("Name").GetComponentInChildren<TMP_Text>().text = referencedItem.itemName;
                    Transform stats = transform.Find("Stats");
                    stats.Find("BrawnNumber").GetComponent<TMP_Text>().text = referencedItem.Brawn.ToString();
                    stats.Find("AgilityNumber").GetComponent<TMP_Text>().text = referencedItem.Agility.ToString();
                    stats.Find("EnduranceNumber").GetComponent<TMP_Text>().text = referencedItem.Endurance.ToString();
                    stats.Find("KnowledgeNumber").GetComponent<TMP_Text>().text = referencedItem.Knowledge.ToString();
                    stats.Find("WisdomNumber").GetComponent<TMP_Text>().text = referencedItem.Wisdom.ToString();
                    stats.Find("CharmNumber").GetComponent<TMP_Text>().text = referencedItem.Charm.ToString();
                    break;
            }
        }

        private void Update()
        {
            if (!isOver)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CloseExamine();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Pointer entered");
            isOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("pointer exited");
            isOver = false;
        }


        private void CloseExamine()
        {
            Destroy(gameObject);
        }
    }
}
