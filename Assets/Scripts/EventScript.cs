using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventScript : MonoBehaviour
{
    public string[] eventText;

    public List<Vector3> navMeshPoint;
    private RollManager RM;

    public Transform[] pointsToMove;

    // Start is called before the first frame update
    void Start()
    {
        RM = FindObjectOfType<RollManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToLocation(Vector3 point)
    {
        FindObjectOfType<PlayerController>().agent.SetDestination(point);
    }

    public void InitiateFight()
    {

    }

    public void rollAbilityCheck(string ability)
    {
        Debug.Log("Rolling a " + ability + " check!");
        RM.rollAbilityCheck(gameObject);
    }
}
