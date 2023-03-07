using System.IO.Pipes;
using UnityEngine;
using System.Linq;

public class AbilityCheckScript : MonoBehaviour, IReceiveResult
{
    PlayerStats ps;
    public int difficulty;
    public string ability;

    private int rolledNumber;
    private int modNumber;

    public GameObject successEvent;
    public GameObject failedEvent;

    public void StartCheck()
    {
        ps = FindObjectOfType<CharacterSheet>().playerStats;
        FindObjectOfType<RollManager>().rollAbilityCheck(gameObject, ability);
        FindObjectOfType<UiAnimatorFinal>().clearingNow(false);
    }

    public void ReceiveResult()
    {   
        Debug.Log(modNumber);
        Debug.Log("Rolled a " + modNumber + ". Has to beat " + difficulty);
        FindObjectOfType<UiAnimatorFinal>().clearSections();
        GameObject eventToSpawn = failedEvent;
        if (modNumber >= difficulty)
        {
            eventToSpawn = successEvent;
        }
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        Canvas explorationCanvas = null;
        foreach(Canvas i in canvases)
        {
            if (i.CompareTag("ExplorationUI")) explorationCanvas = i;
        }
        GameObject spawnedGO = Instantiate(eventToSpawn, explorationCanvas.transform);
        spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimatorFinal>().startingPos;
        Destroy(gameObject);
    }
    public void AbilityCheck()
    {
        if(ps.Stats.Values.Count != 0)
        {
            switch(ability.ToLower())
            {
            case "brawn":
                modNumber = rolledNumber+ ps.GetBonus("BrawnText");
                break;
            case "agility":
                modNumber = rolledNumber+ ps.GetBonus("AgilityText");
                break;
            case "endurance":
                modNumber = rolledNumber+ ps.GetBonus("EnduranceText");
                break; 
            case "wisdom":
                modNumber = rolledNumber+ ps.GetBonus("WisdomText");
                break;
            case "knowledge":
                modNumber = rolledNumber+ ps.GetBonus("KnowledgeText");
                break;
            case "charm":
                modNumber = rolledNumber+ ps.GetBonus("CharmText");
                break;
            default:
                Debug.Log("You spell bad");
                break;         
            }
        }
        else
        {
            modNumber = rolledNumber;
        }
    }
    public void ReceiveRoll(int roll)
    {
        Debug.Log("Roll Received");
        rolledNumber = roll;
        AbilityCheck();
        ReceiveResult();
    }

    
}
