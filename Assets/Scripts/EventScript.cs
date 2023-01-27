using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventScript : MonoBehaviour
{
    public GameObject uiEvent;
    private RollManager RM;
    private uiAnimator uiA;

    public int DifficultyCheck;

    // Start is called before the first frame update
    void Start()
    {
        RM = FindObjectOfType<RollManager>();
        uiA = uiEvent.GetComponent<uiAnimator>();
    }

    public void ActivateEvent()
    {

    }

    public void MoveToLocation(GameObject navPoint)
    {
        navPoint.GetComponent<NavigationPointScript>().MovePlayerHere();
        ClearEvent();
    }

    public void rollAbilityCheck(string ability)
    {
        Debug.Log("Rolling a " + ability.ToLower() + " check!");
        RM.rollAbilityCheck(this);
        ClearEvent();
    }

    public void SucceededRoll()
    {

    }

    public void FailedRoll()
    {

    }

    public void ClearEvent()
    {
        uiA.clearSections();
    }
}
