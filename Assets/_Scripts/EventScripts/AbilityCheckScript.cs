
using UnityEngine;
using System.Linq;
using EnterCloudsReach.Player;

public class AbilityCheckScript : MonoBehaviour, IReceiveResult
{
    private PlayerStats ps;
    public int difficulty;
    public string ability;

    private int rolledNumber;
    private int modNumber;

    public GameObject successEvent;
    public GameObject failedEvent;

     public enum Ability
    {
    None,
    Brawn,
    Agility,
    Endurance,
    Knowledge,
    Wisdom,
    Charm
    }
    [SerializeField]Ability Abilities =  new Ability();
    private void Start()
    {
        ps = FindObjectOfType<PlayerStatDDOL>().playerStats;
    }
    public void StartCheck()
    {
        FindObjectOfType<RollManager>().rollAbilityCheck(gameObject, ability);
        FindObjectOfType<UiAnimatorFinal>().clearingNow(false);
        
    }

    public void ReceiveResult()
    {   
        Debug.Log(modNumber);
        Debug.Log("Rolled a " + modNumber + ". Has to beat " + difficulty);
        GameObject eventToSpawn = failedEvent;
        transform.parent = null;
        FindObjectOfType<UiAnimatorFinal>().clearSections();
        if (modNumber >= difficulty)
        {
            eventToSpawn = successEvent;
        }
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        Canvas explorationCanvas = null;
        foreach(Canvas i in canvases)
        {
            if (i.CompareTag("Exploration")) explorationCanvas = i;
        }
        GameObject spawnedGO = Instantiate(eventToSpawn, explorationCanvas.transform);
        spawnedGO.transform.position = spawnedGO.GetComponent<UiAnimatorFinal>().startingPos;
        Destroy(gameObject);
    }
    public void AbilityCheck()
    {
        if(ps.ModStats.Values.Count != 0)
        {
            switch(Abilities)
            {
            case Ability.None:
                Debug.Log("Choose an actual ability please");
                break;
            case Ability.Brawn:
                modNumber = rolledNumber+ ps.GetBonus("Brawn");
                break;
            case Ability.Agility:
                modNumber = rolledNumber+ ps.GetBonus("Agility");
                break;
            case Ability.Endurance:
                modNumber = rolledNumber+ ps.GetBonus("Endurance");
                break; 
            case Ability.Wisdom:
                modNumber = rolledNumber+ ps.GetBonus("Wisdom");
                break;
            case Ability.Knowledge:
                modNumber = rolledNumber+ ps.GetBonus("Knowledge");
                break;
            case Ability.Charm:
                modNumber = rolledNumber+ ps.GetBonus("Charm");
                break;
            default:
                Debug.Log("You smell");
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
