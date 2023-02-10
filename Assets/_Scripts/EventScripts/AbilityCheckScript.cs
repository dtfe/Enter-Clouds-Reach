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
        Debug.Log("Rolled a " + rolledNumber + ". Has to beat " + difficulty);
        FindObjectOfType<UiAnimator>().clearSections();
        GameObject eventToSpawn = failedEvent;
        if (rolledNumber >= difficulty)
        {
            eventToSpawn = successEvent;
        }
        GameObject spawnedGO = Instantiate(eventToSpawn, FindObjectOfType<Canvas>().transform);
        spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimator>().startingPos;
        //spawnedGO.GetComponent<uiAnimator>().startSection();
    }
}
