using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    private GameObject requestingEvent;
    private int rolledNumber;
    private string rollType;


    public void rollAttack(GameObject requestingSystem)
    {
        requestingEvent = requestingSystem;
        rollType = "attack";
        FindObjectOfType<DieRoller>().RollDie(DieRoller.dFaces.d20);
    }

    public void rollAbilityCheck(GameObject rqEvent, string ability)
    {
        requestingEvent = rqEvent;
        rollType = "ability";
        FindObjectOfType<DieRoller>().RollDie(DieRoller.dFaces.d20);
    }

    public void rollDamage(GameObject requestingSystem, DieRoller.dFaces numbOfFaces)
    {
        requestingEvent = requestingSystem;
        rollType = "damage";
        FindObjectOfType<DieRoller>().RollDie(numbOfFaces);
    }

    public void giveResult(int resultNumber)
    {
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
    }
}
