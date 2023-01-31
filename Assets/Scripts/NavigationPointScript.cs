using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointScript : MonoBehaviour, IReceiveResult
{
    

    public void MovePlayerHere()
    {
        FindObjectOfType<PlayerController>().agent.SetDestination(transform.position);
    }

    public void AbilityCheckBeforeMove(int difficultyCheck, string ability)
    {
        FindObjectOfType<RollManager>().rollAbilityCheck(gameObject, difficultyCheck, ability);

    }

    public void Success()
    {
        MovePlayerHere();
    }

    public void Failure()
    {

    }
}
