using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPointScript : MonoBehaviour
{
    [Header("Point Explanation")]
    [TextArea] public string desc;

    [Header("Parameters")]
    public string navPointId;

    public GameObject Event;


    [SerializeField]private Canvas canvas;

    [SerializeField]private Animator anim;

    private void Start()
    {
        
    }

    public void MovePlayerHere()
    {
        if (anim) anim.enabled = true;
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
            spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimatorFinal>().startingPos;
            //spawnedGO.GetComponent<uiAnimator>().startSection();
        }
        else   
        {
            canvas = FindObjectOfType<Canvas>();
            Debug.Log("Navpoint: " + navPointId + " has triggered it's event");
            GameObject spawnedGO = Instantiate(Event, canvas.transform);
            spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimatorFinal>().startingPos;
            //spawnedGO.GetComponent<uiAnimator>().startSection();
        }
    }
}
