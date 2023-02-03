using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextEvent : MonoBehaviour
{
    public int numbOfActivations;
    public PlayerInput playerInput;
    private InputAction clickAction;
    public bool clearEvents;
    public GameObject nextEvent;
    private uiAnimator uiAnim;
    // Start is called before the first frame update
    void Start()
    {   
        uiAnim = GetComponentInParent<uiAnimator>();
    }

    // Update is called once per frame
    void ClickInput(InputAction.CallbackContext inn)
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
    void OnEnable()
    {
        clickAction = playerInput.actions.FindAction("Click");
        clickAction.performed += ClickInput;
        clickAction.canceled += ClickInput;
    }
    void OnDisable()
    {
        clickAction = playerInput.actions.FindAction("Click");
        clickAction.performed -= ClickInput;
        clickAction.canceled -= ClickInput;
    }
}
