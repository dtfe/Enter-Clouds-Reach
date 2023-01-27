using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    private EventScript requestingEvent;
    private int rolledNumber;
    private string rollType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rollAbilityCheck(EventScript rqEvent)
    {
        requestingEvent = rqEvent;
        rollType = "ability";
        FindObjectOfType<dieRoller>().RollDie(dieRoller.dFaces.d20);
    }

    public void rollDamage()
    {
        rollType = "damage";
        FindObjectOfType<dieRoller>().RollDie(dieRoller.dFaces.d6);
    }

    public void giveResult(int resultNumber)
    {
        rolledNumber = resultNumber;
        switch (rollType)
        {
            case "ability":
                if (rolledNumber >= requestingEvent.DifficultyCheck)
                {
                    requestingEvent.SucceededRoll();
                }
                else
                {
                    requestingEvent.FailedRoll();
                }
                break;

        }
    }
}
