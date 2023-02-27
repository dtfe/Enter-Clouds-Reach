using UnityEngine;

public class AbilityCheckScript : MonoBehaviour, IReceiveResult
{
    public int difficulty;
    public string ability;

    private int rolledNumber;

    public GameObject successEvent;
    public GameObject failedEvent;

    public void StartCheck()
    {
        FindObjectOfType<RollManager>().rollAbilityCheck(gameObject, ability);
        FindObjectOfType<UiAnimator>().clearingNow(false);
    }

    public void ReceiveResult()
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
        Destroy(gameObject);
    }

    public void ReceiveRoll(int roll)
    {
        rolledNumber = roll;
        ReceiveResult();
    }

    
}
