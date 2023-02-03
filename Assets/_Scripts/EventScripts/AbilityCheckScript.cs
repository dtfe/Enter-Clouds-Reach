using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCheckScript : MonoBehaviour
{
    public int difficulty;
    public string ability;

    public GameObject successEvent;
    public GameObject failedEvent;

    public void StartCheck()
    {
        FindObjectOfType<RollManager>().rollAbilityCheck(gameObject, ability);
    }

    public void ReceiveResult(int rolledNumber)
    {
        GameObject eventToSpawn = failedEvent;
        if(rolledNumber >= difficulty)
        {
            eventToSpawn = successEvent;
        }
        GameObject spawnedGO = Instantiate(eventToSpawn, FindObjectOfType<Canvas>().transform);
        spawnedGO.transform.position = spawnedGO.GetComponent<uiAnimator>().startingPos;
        spawnedGO.GetComponent<uiAnimator>().startSection();
    }
}
