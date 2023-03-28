using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace EnterCloudsReach.Inventory
{
    public class AbilityPopupInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public GameObject infoPopup;

        public string info;

        public float timeToSpawnInfo;

        private GameObject activePopup;

        private bool isHovering;

        private float timer;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Mouse has entered: " + gameObject.name);
            timer = timeToSpawnInfo;
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
            Debug.Log("Mouse has exited: " + gameObject.name);
            if (activePopup)
            {
                Destroy(activePopup);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isHovering && !activePopup)
            {
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    activePopup = Instantiate(infoPopup, transform);
                    float xpos = GetComponent<RectTransform>().rect.width / 2 + 177;
                    activePopup.GetComponent<RectTransform>().anchoredPosition = new Vector2(xpos, 0);
                    activePopup.GetComponentInChildren<TMP_Text>().text = info;
                }
            }
        }
    }
}
