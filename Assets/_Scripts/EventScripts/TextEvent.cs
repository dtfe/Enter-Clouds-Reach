
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextEvent : MonoBehaviour
{
    public int numbOfActivations;
    private PlayerInput playerInput;
    private InputAction clickAction;
    public bool clearEvents;
    public GameObject nextEvent;
    private UiAnimator uiAnim;
    // Start is called before the first frame update
    void Awake()
    {
        uiAnim = GetComponentInParent<UiAnimator>();
        playerInput = FindObjectOfType<PlayerController>().GetComponent<PlayerInput>();
    }

    
    void ClickInput(InputAction.CallbackContext inn)
    { 
        if(!uiAnim.isMoving)
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
        if (GetComponent<AbilityCheckScript>())
        {
            GetComponent<AbilityCheckScript>().StartCheck();
        }
        if (clearEvents)
        {
            //uiAnim.clearSections();
            Destroy(this);
        }
        else
        {
            //nextEvent.GetComponent<TextEvent>().activateNext();
            Destroy(this);
        }
    }
    void OnEnable()
    {
        if (playerInput != null)
        {
            clickAction = playerInput.actions.FindAction("Click");
            clickAction.performed += ClickInput;
            clickAction.canceled += ClickInput;
        }
    }
    void OnDisable()
    {
        if (playerInput != null)
        {
            clickAction = playerInput.actions.FindAction("Click");
            clickAction.performed -= ClickInput;
            clickAction.canceled -= ClickInput;
        }
    }
}
