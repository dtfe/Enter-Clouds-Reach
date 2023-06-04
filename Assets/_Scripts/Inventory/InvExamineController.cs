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

        private float textTimer;
        private bool skipped = false;
        private string notesTextToDisplay;
        private TMP_Text notesText;

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
                    notesTextToDisplay = transform.Find("Text").GetComponent<TMP_Text>().text = referencedItem.notes;
                    notesText = transform.Find("Text").GetComponent<TMP_Text>();
                    notesText.text = "";
                    StartCoroutine(WriteLetters());
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
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    skipped = true;
                }
            }
        }

        IEnumerator WriteLetters()
        {
            for(int i = 0; i < notesTextToDisplay.Length; i++)
            {
                if (skipped)
                {
                    notesText.text = notesTextToDisplay;
                    break;
                }
                notesText.text += notesTextToDisplay[i];
                if (notesTextToDisplay[i] == 46)
                {
                    yield return new WaitForSeconds(0.2f);
                } else if (notesTextToDisplay[i] == 44)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.025f);
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
