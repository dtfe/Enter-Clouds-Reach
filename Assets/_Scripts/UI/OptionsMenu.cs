using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach
{
    public class OptionsMenu : MonoBehaviour
    {
        GameObject Options;
        RectTransform m_rectTransform;
        [SerializeField]Vector2 previousPos,startingPos,targetPos;
        bool isMoving;
        float t=0;
        // Start is called before the first frame update
        void Start()
        {
            Options = this.gameObject;
            m_rectTransform = gameObject.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isMoving)
            {
                previousPos = m_rectTransform.anchoredPosition;
                t = 0;
            }
            else
            {
                t += Time.deltaTime * 0.9f;
                Vector2 moveUp = Vector2.Lerp(previousPos, targetPos, t);
                m_rectTransform.anchoredPosition = moveUp;
                if (m_rectTransform.anchoredPosition == targetPos)
                {
                    isMoving = false;
                }
            }
        }
        public void setMoving()
        {
            if(!isMoving && previousPos == startingPos)
            {
                isMoving = true;
            }
            else
            {
                m_rectTransform.anchoredPosition = startingPos;
            }
        }
    }
}
