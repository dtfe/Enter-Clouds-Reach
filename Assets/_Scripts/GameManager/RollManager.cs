using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    private GameObject requestingEvent;
    private DieRoller DR;
    public PlayerStats ps;
    private int rolledNumber;
    private string rollType;
    private int mod;

    private void Start()
    {
        DR = FindObjectOfType<DieRoller>();
    }


    public void rollAttack(GameObject requestingSystem)
    {
        requestingEvent = requestingSystem;
        rollType = "attack";
        if (DR == null)
        {
            DR = FindObjectOfType<DieRoller>();
        }
        DR.RollDie(DieRoller.dFaces.d20);
    }

    public void rollAbilityCheck(GameObject rqEvent, string ability)
    {
        requestingEvent = rqEvent;
        rollType = "ability";
        if (DR == null)
        {
            DR = FindObjectOfType<DieRoller>();
        }
        DR.RollDie(DieRoller.dFaces.d20);
    }

    public void rollDamage(GameObject requestingSystem, DieRoller.dFaces numbOfFaces)
    {
        requestingEvent = requestingSystem;
        rollType = "damage";
        if (DR == null)
        {
            DR = FindObjectOfType<DieRoller>();
        }
        DR.RollDie(DieRoller.dFaces.d20);
    }

    public void giveResult(int resultNumber)
    {
        requestingEvent.GetComponent<IReceiveResult>().ReceiveRoll(resultNumber);
        /*
        switch (rollType)
        {
            case "ability":
                requestingEvent.GetComponent<AbilityCheckScript>().ReceiveResult(resultNumber);
                break;

            case "attack":
                requestingEvent.GetComponent<BattleSystem>().ReceiveRoll(resultNumber);
                break;

            case "damage":
                requestingEvent.GetComponent<BattleSystem>().ReceiveRoll(resultNumber);
                break;
        }
        */
    }
}
