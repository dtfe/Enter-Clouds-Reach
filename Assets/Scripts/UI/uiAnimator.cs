using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiAnimator : MonoBehaviour
{
    float t = 0;
    private bool isMoving = false;

    private RectTransform m_RectTransform;
    private Vector2 previousPos;
    private Vector2 startingPos;
    private Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        startingPos = m_RectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            nextSection();
        }

        if (!isMoving)
        {
            previousPos = m_RectTransform.anchoredPosition;
        }
        else
        {
            t += Time.deltaTime * 0.9f;
            Vector2 moveUp = Vector2.Lerp(previousPos, targetPos, t);
            m_RectTransform.anchoredPosition = moveUp;
            if (m_RectTransform.anchoredPosition == targetPos)
            {
                isMoving = false;
            }
        }
    }

    private void nextSection()
    {
        t = 0;
        targetPos = new Vector2(previousPos.x, previousPos.y + 240);
        isMoving = true;
    }

    public void clearSections()
    {
        t = 0;
        targetPos = startingPos;
        isMoving = true;
    }
}
