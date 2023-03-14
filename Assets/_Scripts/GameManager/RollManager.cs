using UnityEngine;

public class RollManager : MonoBehaviour
{
    private GameObject requestingEvent;
    private DieRoller DR;
    public PlayerStats ps;

    private void Start()
    {
        DR = FindObjectOfType<DieRoller>();
    }


    public void rollAttack(GameObject requestingSystem)
    {
        requestingEvent = requestingSystem;
        if (DR = null)
        {
            DR = FindObjectOfType<DieRoller>();
        }
        DR.RollDie(DieRoller.dFaces.d20);
    }

    public void rollAbilityCheck(GameObject rqEvent, string ability)
    {
        requestingEvent = rqEvent;
        if (DR = null)
        {
            DR = FindObjectOfType<DieRoller>();
        }
        DR.RollDie(DieRoller.dFaces.d20);
    }

    public void rollDamage(GameObject requestingSystem, DieRoller.dFaces numbOfFaces)
    {
        requestingEvent = requestingSystem;
        if (DR = null)
        {
            DR = FindObjectOfType<DieRoller>();
        }
        DR.RollDie(DieRoller.dFaces.d20);
    }

    public void giveResult(int resultNumber)
    {
        requestingEvent.GetComponent<IReceiveResult>().ReceiveRoll(resultNumber);
    }
}
