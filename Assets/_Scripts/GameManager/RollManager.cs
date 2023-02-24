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
        FindObjectOfType<dieRoller>().RollDie(dieRoller.dFaces.d20);
    }

    public void rollAbilityCheck(GameObject rqEvent, string ability)
    {
        requestingEvent = rqEvent;
        rollType = "ability";
        FindObjectOfType<dieRoller>().RollDie(dieRoller.dFaces.d20);
    }

    public void rollDamage(GameObject requestingSystem, dieRoller.dFaces numbOfFaces)
    {
        requestingEvent = requestingSystem;
        rollType = "damage";
        FindObjectOfType<dieRoller>().RollDie(numbOfFaces);
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
