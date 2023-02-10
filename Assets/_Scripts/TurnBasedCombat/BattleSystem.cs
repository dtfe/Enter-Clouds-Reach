using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState 
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST


}

public class BattleSystem : MonoBehaviour
{
    [Header("Properties")]
    public TMP_Text dialogue;
    public GameObject actions;

    [Header("Player Properties")]
    public GameObject playerPrefab;
    public Transform playerSpawn;

    [Header("Enemy Properties")]
    public GameObject enemyPrefab;
    public Transform enemySpawn;

    private BattleHUD playerHUD;
    private BattleHUD enemyHUD;

    [Header("Game Information")]
    public BattleState state;

    Unit playerUnit;
    Unit enemyUnit;


    private int rolledNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();
        playerHUD = playerGO.transform.Find("HUD").GetComponent<BattleHUD>();

        playerHUD.SetHUD(playerUnit);

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyHUD = enemyGO.transform.Find("HUD").GetComponent<BattleHUD>();

        enemyHUD.SetHUD(enemyUnit);

        rolledNumber = 0;

        yield return new WaitForSeconds(0f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack1()
    {
        // Temporary attack stuff
        bool attackHit = false;
        if (rolledNumber + playerUnit.attackBonus >= enemyUnit.defense)
        {
            attackHit = true;
        }

        if (attackHit)
        {
            dialogue.text = playerUnit.unitName + " dealt 1 damage!";
            enemyUnit.takeDamage(playerUnit.damage);
            enemyHUD.SetHP(enemyUnit.curHP);
        }

        rolledNumber = 0;
        // Play Animation?
        yield return new WaitForSeconds(2f);


        // Check if Enemy is dead
        if (enemyUnit.curHP <= 0)
        {
            //If true player wins and moves on
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            //if false, turn order moves on to the enemy's turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            // Win!
            PlayerPrefs.SetString("BattleResult", "Won");
        } else if (state == BattleState.LOST)
        {
            // Lost!
            PlayerPrefs.SetString("BattleResult", "Lost");
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        dialogue.text = enemyUnit.unitName + " is attacking " + playerUnit.unitName + "!";
        yield return new WaitForSeconds(2f);
        FindObjectOfType<RollManager>().rollAttack(gameObject);
        yield return new WaitUntil(() => rolledNumber != 0);
        if (rolledNumber + enemyUnit.defense >= playerUnit.defense)
        {
            playerUnit.takeDamage(enemyUnit.damage);
            playerHUD.SetHP(playerUnit.curHP);
            dialogue.text = playerUnit.unitName + " has taken " + enemyUnit.damage.ToString() + " damage!";
        }
        yield return new WaitForSeconds(2);

        rolledNumber = 0;
        if (playerUnit.curHP <= 0)
        {
            state = BattleState.LOST;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }



    private void PlayerTurn()
    {
        dialogue.text = "It's your turn!";
        actions.SetActive(true);
    }

    public void Attack(int attackNumber)
    {
        dialogue.text = playerUnit.unitName + " is Attacking " + enemyUnit.unitName + "!";
        FindObjectOfType<RollManager>().rollAttack(gameObject);
        StartCoroutine(OnAttackSelection(attackNumber));
        actions.SetActive(false);

    }

    IEnumerator OnAttackSelection(int attackNumber)
    {
        Debug.Log("Waiting for Result");
        yield return new WaitUntil(() => rolledNumber != 0);
        Debug.Log("Attacking!");
        switch (attackNumber)
        {
            case 1:
                StartCoroutine(PlayerAttack1());
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;
        }
        
    }

    public void ReceiveRoll(int roll)
    {
        Debug.Log("Received Result");
        rolledNumber = roll;
    }
}
