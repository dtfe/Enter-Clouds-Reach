using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointScript : MonoBehaviour
{
    public string navPointId;

    public GameObject Event;

    [SerializeField]private Canvas canvas;

    [SerializeField]private Animator anim;

    private void Start()
    {
        
    }

    public void MovePlayerHere()
    {
        FindObjectOfType<PlayerController>().agent.SetDestination(transform.position);
        FindObjectOfType<PlayerController>().SetTargetPos(gameObject);
    }

    public void TriggerEvent()
    {
        Debug.Log("Triggered Event: " + navPointId);
        if(canvas != null)
        {
            Debug.Log("Navpoint: " + navPointId + " has triggered it's event");
            GameObject spawnedGO = Instantiate(Event, canvas.transform);
            spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimator>().startingPos;
            //spawnedGO.GetComponent<uiAnimator>().startSection();
        }
        else   
        {
            canvas = FindObjectOfType<Canvas>();
            Debug.Log("Navpoint: " + navPointId + " has triggered it's event");
            GameObject spawnedGO = Instantiate(Event, canvas.transform);
            spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimator>().startingPos;
            //spawnedGO.GetComponent<uiAnimator>().startSection();
        }
    }
}
