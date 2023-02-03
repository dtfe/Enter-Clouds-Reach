using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEvent : MonoBehaviour
{
    public int numbOfActivations;

    public bool clearEvents;

    public GameObject nextEvent;
    private uiAnimator uiAnim;
    // Start is called before the first frame update
    void Start()
    {
        uiAnim = GetComponentInParent<uiAnimator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            switch (numbOfActivations)
            {
                case 1:
                    uiAnim.nextSection();
                    activateNext();
                    break;
                case 2:
                    NextAction();
                    break;
            }
        }
    }

    public void activateNext()
    {
        numbOfActivations++;
    }

    private void NextAction()
    {
        if (clearEvents)
        {
            uiAnim.clearSections();
            Destroy(this);
        }
        else
        {
            nextEvent.GetComponent<TextEvent>().activateNext();
            Destroy(this);
        }
    }
}
