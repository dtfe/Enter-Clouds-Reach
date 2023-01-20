using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    private GameObject requestingEvent;
    private int rolledNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rollAbilityCheck()
    {
        FindObjectOfType<dieRoller>().RollDie(dieRoller.dFaces.d20);
    }

    public void rollDamage()
    {
        FindObjectOfType<dieRoller>().RollDie(dieRoller.dFaces.d6);
    }

    public void giveResult(int resultNumber)
    {
        rolledNumber = resultNumber;
    }
}
