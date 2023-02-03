using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointScript : MonoBehaviour
{
    public GameObject Event;

    // Leave these alone if there is no roll connected to this navpoint
    public GameObject successEvent;
    public GameObject failureEvent;

    private Canvas canvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void MovePlayerHere()
    {
        FindObjectOfType<PlayerController>().agent.SetDestination(transform.position);
        FindObjectOfType<PlayerController>().SetTargetPos(gameObject);
    }

    public void TriggerEvent()
    {
        GameObject spawnedGO = Instantiate(Event, canvas.transform);
        spawnedGO.transform.position = spawnedGO.GetComponent<uiAnimator>().startingPos;
        spawnedGO.GetComponent<uiAnimator>().startSection();
    }
}
