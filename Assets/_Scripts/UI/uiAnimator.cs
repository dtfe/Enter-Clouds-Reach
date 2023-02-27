using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiAnimator : MonoBehaviour
{
    float t = 0;
    [HideInInspector]public bool isMoving = false;
    private bool clearing = false;
    bool abilityCheck = false;
    public GameObject startingBox;

    private RectTransform m_RectTransform;
    public Vector2 previousPos;
    public Vector2 startingPos;
    public Vector2 targetPos;
    // Start is called before the first frame update
    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_RectTransform.SetLocalPositionAndRotation(new Vector3(0, transform.position.y), Quaternion.identity);
        startSection();
        nextSection();
        startingPos = m_RectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
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
                if (clearing)
                {
                    Destroy(gameObject);
                }
                if(abilityCheck)
                {
                    m_RectTransform.anchoredPosition = new Vector2(0,-138);
                }
            }
        }
    }

    public void startSection()
    {
        startingBox.GetComponent<TextEvent>().activateNext();
    }

    public void nextSection()
    {
        t = 0;
        targetPos = new Vector2(previousPos.x, previousPos.y + 250);
        isMoving = true;
    }

    public void AbilityCheck()
    {
        t = 0;
        targetPos = startingPos;
        isMoving = true;
        clearing = false;
        abilityCheck = true;
    }
    public void clearSections()
    {
        t = 0;
        targetPos = startingPos;
        isMoving = true;
        clearing = true;

    }

    public void clearingNow(bool clear)
    {
        clearing = clear;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
