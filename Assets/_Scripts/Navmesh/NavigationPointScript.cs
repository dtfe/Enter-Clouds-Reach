using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointScript : MonoBehaviour
{
    public string navPointId;

    public GameObject Event;

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
        if(canvas != null)
        {
            GameObject spawnedGO = Instantiate(Event, canvas.transform);
            spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimator>().startingPos;
            //spawnedGO.GetComponent<uiAnimator>().startSection();
        }
        else   
        {
            canvas = FindObjectOfType<Canvas>();
        }
    }
}
