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

public class BattleSystem : MonoBehaviour, IReceiveResult
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

    private RollManager rm;

    Unit playerUnit;
    Unit enemyUnit;

    private int rolledNumber = 0;
    private bool criticalHit = false;
    private bool criticalMiss = false;

    // Start is called before the first frame update
    void Start()
    {
        rm = FindObjectOfType<RollManager>();
        //StartCoroutine(SetupBattle());
    }

    #region battleSetup
    public void startSetup()
    {
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        state = BattleState.START;

        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Unit>();
        playerHUD = playerGO.transform.Find("HUD").GetComponent<BattleHUD>();

        playerHUD.SetHUD(playerUnit);

        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyHUD = enemyGO.transform.Find("HUD").GetComponent<BattleHUD>();

        enemyHUD.SetHUD(enemyUnit);

        rolledNumber = 0;

        yield return new WaitForSeconds(0f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }
    #endregion battleSetup

    #region playerMethods
    IEnumerator PlayerTurn()
    { 
        if (!playerUnit.poisonImmune && playerUnit.poison > 1)
        {
            dialogue.text = "Player has taken " + playerUnit.takeDamage(playerUnit.poison, false) + " bleed damage!";
            playerUnit.poison = playerUnit.poison / 2;
            yield return new WaitForSeconds(1);
        }
        if (CheckStatuses(playerUnit, statusEffects.Stunned))
        {
            rm.rollAbilityCheck(gameObject, "endurance");
            yield return new WaitUntil(() => rolledNumber != 0);
            if (rolledNumber <= playerUnit.stunned)
            {
                rolledNumber = 0;
                dialogue.text = "Player was stunned!";
                playerUnit.addStatus(statusEffects.Stunned, -playerUnit.stunned);
                yield return new WaitForSeconds(1);
                StartCoroutine(EnemyTurn());
                yield break;
            }
        }
        dialogue.text = "It's your turn!";
        actions.SetActive(true);
    }

    public void Attack(int attackNumber)
    {
        rolledNumber = 0;
        dialogue.text = playerUnit.unitName + " is attacking " + enemyUnit.unitName + "!";
        FindObjectOfType<RollManager>().rollAttack(gameObject);
        actions.SetActive(false);
        StartCoroutine(OnAttackSelection(attackNumber));

    }

    IEnumerator OnAttackSelection(int attackNumber)
    {
        Debug.Log("Waiting for Result");
        yield return new WaitUntil(() => rolledNumber != 0);
        criticalMiss = false;
        criticalHit = false;
        if (rolledNumber == 1)
        {
            criticalMiss = true;
        }
        else if (rolledNumber == 20)
        {
            criticalHit = true;
        }
        Debug.Log("Attacking!");
        switch (attackNumber)
        {
            case 1:
                StartCoroutine(PlayerAttack1());
                break;

            case 2:
                StartCoroutine(PlayerAttack2());
                break;

            case 3:
                break;

            case 4:
                break;
        }

    }

    IEnumerator PlayerAttack1()
    {
        // attack stuff
        dialogue.text = playerUnit.unitName + " has rolled " + rolledNumber + " + " + playerUnit.attackBonus + " for a total of " + (rolledNumber + playerUnit.attackBonus);
        yield return new WaitForSeconds(1);
        if (rolledNumber + playerUnit.attackBonus >= enemyUnit.defense)
        {
            dialogue.text = playerUnit.unitName + " beat the opponent's defense!";
            rolledNumber = 0;
            rm.rollDamage(gameObject, playerUnit.damage);
            yield return new WaitUntil(() => rolledNumber != 0);
            dialogue.text = playerUnit.unitName + " dealt " + enemyUnit.takeDamage(rolledNumber + playerUnit.damageBonus, criticalHit) + " damage!";
            criticalHit = false;
        }

        rolledNumber = 0;
        // Play Animation?
        Debug.Log("Attack finished");
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

    IEnumerator PlayerAttack2()
    {
        // attack stuff
        dialogue.text = playerUnit.unitName + " has rolled " + rolledNumber + " + " + (playerUnit.attackBonus - 4) + " for a total of " + (rolledNumber + playerUnit.attackBonus - 4);
        yield return new WaitForSeconds(1);
        if (rolledNumber + playerUnit.attackBonus - 4 >= enemyUnit.defense)
        {
            dialogue.text = playerUnit.unitName + " beat the opponent's defense!";
            rolledNumber = 0;
            rm.rollDamage(gameObject, playerUnit.damage);
            yield return new WaitUntil(() => rolledNumber != 0);
            dialogue.text = playerUnit.unitName + " dealt " + enemyUnit.takeDamage(rolledNumber + playerUnit.damageBonus, criticalHit) + " damage!";
            enemyHUD.SetHP(enemyUnit.curHP);
            criticalHit = false;
            yield return new WaitForSeconds(2);
            dialogue.text = enemyUnit.name + " has been hit by a stunning strike!";
            enemyUnit.addStatus(statusEffects.Stunned, 5);
        }

        rolledNumber = 0;
        // Play Animation?
        yield return new WaitForSeconds(1f);


        // Check if Enemy is dead
        if (enemyUnit.curHP <= 0)
        {
            //If true player wins and moves on
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(PlayerEndOfTurn());
        }
    }

    IEnumerator PlayerEndOfTurn()
    {
        if (!playerUnit.bleedImmune && playerUnit.bleed > 0)
        {
            dialogue.text = "Player has taken " + playerUnit.takeDamage(playerUnit.bleed, false) + " bleed damage";
        }
        yield return new WaitForSeconds(1);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    #endregion playerMethods

    #region enemyMethods
    IEnumerator EnemyTurn()
    {
        if (CheckStatuses(enemyUnit, statusEffects.Poison))
        {
            dialogue.text = "Enemy has taken " + playerUnit.takeDamage(enemyUnit.poison, false) + " poison damage!";
            enemyUnit.poison = enemyUnit.poison / 2;
        }
        if (CheckStatuses(enemyUnit, statusEffects.Stunned))
        {
            rm.rollAbilityCheck(gameObject, "endurance");
            yield return new WaitUntil(() => rolledNumber != 0);
            if (rolledNumber <= enemyUnit.stunned)
            {
                dialogue.text = "Enemy's was successfully stunned!";
                enemyUnit.addStatus(statusEffects.Stunned, -enemyUnit.stunned);
                yield return new WaitForSeconds(1);
                StartCoroutine(PlayerTurn());
                yield break;
            }
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        rolledNumber = 0;
        dialogue.text = enemyUnit.unitName + " is attacking " + playerUnit.unitName + "!";
        yield return new WaitForSeconds(2f);

        FindObjectOfType<RollManager>().rollAttack(gameObject);
        yield return new WaitUntil(() => rolledNumber != 0);
        criticalMiss = false;
        criticalHit = false;
        if (rolledNumber == 1)
        {
            criticalMiss = true;
        }
        else if (rolledNumber == 20)
        {
            criticalHit = true;
        }
        if (criticalMiss)
        {
            dialogue.text = enemyUnit.unitName + " has critically missed!";
        }
        else
        {
            dialogue.text = enemyUnit.unitName + " has rolled a " + rolledNumber + " + " + enemyUnit.attackBonus + " for a total of " + (rolledNumber + enemyUnit.attackBonus);
        }
        yield return new WaitForSeconds(2f);
        if (rolledNumber + enemyUnit.defense >= playerUnit.defense && !criticalMiss)
        {
            if (criticalHit)
            {
                dialogue.text = enemyUnit.unitName + " has critically hit the hero!";
            }
            else
            {
                dialogue.text = enemyUnit.unitName + " beat the hero's defense!";
            }
            rolledNumber = 0;
            rm.rollDamage(gameObject, enemyUnit.damage);
            yield return new WaitUntil(() => rolledNumber != 0);
            dialogue.text = playerUnit.unitName + " has taken " + playerUnit.takeDamage(rolledNumber + enemyUnit.damageBonus, criticalHit) + " damage!";
            playerHUD.SetHP(playerUnit.curHP);
            yield return new WaitForSeconds(2);
        }

        rolledNumber = 0;
        if (playerUnit.curHP <= 0)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(EnemyEndOfTurn());
        }
    }

    IEnumerator EnemyEndOfTurn()
    {
        if (!enemyUnit.bleedImmune && enemyUnit.bleed > 0)
        {
            dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(enemyUnit.bleed, false) + " bleed damage!";
        }
        yield return new WaitForSeconds(1);
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }
    #endregion enemyMethods

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            // Win!
            dialogue.text = playerUnit.unitName + " has defeated the enemy!";
            PlayerPrefs.SetString("BattleResult", "Won");
        }
        else if (state == BattleState.LOST)
        {
            // Lost!
            dialogue.text = playerUnit.unitName + " has been defeated!";
            PlayerPrefs.SetString("BattleResult", "Lost");
        }
        yield return new WaitForSeconds(2f);
        FindObjectOfType<BattleLoader>().EndBattle();
    }

    private bool CheckStatuses(Unit unitToCheck, statusEffects status)
    {
        switch (status)
        {
            default:
                Debug.Log("Status was not found, returning false");
                return false;
                

            case statusEffects.Stunned:
                if(unitToCheck.stunned > 1)
                {
                    return true;
                }
                return false;

            case statusEffects.Bleed:
                if(!unitToCheck.bleedImmune && unitToCheck.bleed > 1)
                {
                    return true;
                }
                return false;

            case statusEffects.Poison:
                if (!unitToCheck.poisonImmune && unitToCheck.poison > 1)
                {
                    return true;
                }
                return false;
        }
    }

    public void ReceiveRoll(int roll)
    {
        Debug.Log("Received Result");
        rolledNumber = roll;
    }
}
