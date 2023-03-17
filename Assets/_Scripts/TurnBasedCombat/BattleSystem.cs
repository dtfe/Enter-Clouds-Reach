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

    [Header("Attack Prefabs")]
    public GameObject[] attack;

    [Header("Player Properties")]
    public GameObject playerPrefab;
    public Transform playerSpawn;

    [Header("Enemy Properties")]
    public GameObject enemyPrefab;
    public Transform enemySpawn;
    public List<DefendTimings> enemyTimings = new List<DefendTimings>();

    private BattleHUD playerHUD;
    private BattleHUD enemyHUD;

    [Header("Game Information")]
    public BattleState state;
    public bool startBattleOnStart = false;

    private RollManager rm;

    Unit playerUnit;
    Unit enemyUnit;
    

    // Start is called before the first frame update
    void Start()
    {
        rm = FindObjectOfType<RollManager>();

        if (startBattleOnStart)
        {
            StartCoroutine(SetupBattle());
        }
    }

    #region battleSetup
    public void startSetup()
    {
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        state = BattleState.START;
        if (playerUnit == null)
        {
            GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
            playerUnit = playerGO.GetComponent<Unit>();
            playerHUD = playerGO.transform.Find("HUD").GetComponent<BattleHUD>();
            //This wont work with saving
            PlayerPrefs.SetInt("playerHealth", playerUnit.maxHP);
        }
        playerUnit.curHP = PlayerPrefs.GetInt("playerHealth");

        playerHUD.SetHUD(playerUnit);

        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyHUD = enemyGO.transform.Find("HUD").GetComponent<BattleHUD>();
        for (int i = 0; i < enemyUnit.defendTimings.Length; i++)
        {
            for (int j = 0; j < enemyUnit.defendTimings[i].timingWeight; j++)
            {
                enemyTimings.Add(enemyUnit.defendTimings[i]);
                Debug.Log("Added Timing: " + enemyUnit.defendTimings[i].timing.name);
            }
        }

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(0f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }
    #endregion battleSetup

    #region playerMethods
    IEnumerator PlayerTurn()
    { 
        if (CheckStatuses(playerUnit, statusEffects.Poison))
        {
            dialogue.text = "You've taken " + playerUnit.takeDamage(playerUnit.poison) + " poison damage!";
            if (playerUnit.poison == 1)
            {
                playerUnit.addStatus(statusEffects.Poison, -1);
            }
            else
            {
                playerUnit.addStatus(statusEffects.Poison, (int)Mathf.Floor(-playerUnit.poison / 2f));
            }
            yield return new WaitForSeconds(1);
        }
        if (CheckStatuses(playerUnit, statusEffects.Stunned))
        {
            if (playerUnit.stunned >= 1)
            {
                dialogue.text = "You've been stunned";
                playerUnit.addStatus(statusEffects.Stunned, -1);
                yield return new WaitForSeconds(1);
                StartCoroutine(PlayerEndOfTurn());
                yield break;
            }
        }
        if (CheckHealth())
        {
            yield break;
        }
        dialogue.text = "It's your turn!";
        actions.SetActive(true);
    }

    public void Attack(int attackNumber)
    {
        dialogue.text = playerUnit.unitName + " is attacking " + enemyUnit.unitName + "!";
        //FindObjectOfType<RollManager>().rollAttack(gameObject);
        actions.SetActive(false);
        StartCoroutine(PlayerAttack(attackNumber - 1));
        //StartCoroutine(OnAttackSelection(attackNumber));

    }

    IEnumerator OnAttackSelection(int attackNumber)
    {
        #region old
        //Debug.Log("Waiting for Result");
        //yield return new WaitUntil(() => rolledNumber != 0);
        /*criticalMiss = false;
        criticalHit = false;
        if (rolledNumber == 1)
        {
            criticalMiss = true;
        }
        else if (rolledNumber == 20)
        {
            criticalHit = true;
        }*/
        #endregion old 
        yield return new WaitForSeconds(0);
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

    IEnumerator PlayerAttack(int attackIndex)
    {
        GameObject timingGO = Instantiate(attack[attackIndex], actions.transform.parent);
        TimingController timingCon = timingGO.GetComponent<TimingController>();
        timingCon.startTiming(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || !timingCon.isActivated);
        playerUnit.animationStart("playerAttacking");
        HitTiming hitStatus = timingCon.Clicked();
        switch (hitStatus)
        {
            case (HitTiming.Miss):
                dialogue.text = "You missed your attack!";
                yield return new WaitForSeconds(1);
                break;

            case (HitTiming.Hit):
                dialogue.text = "Hit!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.damage) + " damage!";
                yield return new WaitForSeconds(1);
                enemyUnit.addStatus(timingCon.regularHitStatus, timingCon.hitStatusStacks);
                yield return new WaitForSeconds(1);
                break;

            case (HitTiming.Critical):
                dialogue.text = "Critical Hit!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.CritDamage) + " damage!";
                yield return new WaitForSeconds(1);
                enemyUnit.addStatus(timingCon.criticalHitStatus, timingCon.criticalStatusStacks);
                break;
        }
        Destroy(timingGO);

        //rolledNumber = 0;
        // Play Animation?
        Debug.Log("Attack finished");
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
            //if false, turn order moves on to the enemy's turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(PlayerEndOfTurn());
        }
    }

    IEnumerator PlayerAttack1()
    {

        // attack stuff
        #region old
        /*
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
        }*/
        #endregion old
        GameObject timingGO = Instantiate(attack[0], actions.transform.parent);
        TimingController timingCon = timingGO.GetComponent<TimingController>();
        timingCon.startTiming(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || !timingCon.isActivated);
        playerUnit.animationStart("attacking");
        HitTiming hitStatus = timingCon.Clicked();
        switch (hitStatus)
        {
            case (HitTiming.Miss):
                dialogue.text = "You missed your attack!";
                yield return new WaitForSeconds(1);
                break;

            case (HitTiming.Hit):
                dialogue.text = "Hit!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.damage) + " damage!";
                yield return new WaitForSeconds(1);
                enemyUnit.addStatus(timingCon.regularHitStatus, timingCon.hitStatusStacks);
                yield return new WaitForSeconds(1);
                break;

            case (HitTiming.Critical):
                dialogue.text = "Critical Hit!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.CritDamage) + " damage!";
                yield return new WaitForSeconds(1);
                enemyUnit.addStatus(timingCon.criticalHitStatus, timingCon.criticalStatusStacks);
                break;
        }
        Destroy(timingGO);

        //rolledNumber = 0;
        // Play Animation?
        Debug.Log("Attack finished");
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
            //if false, turn order moves on to the enemy's turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(PlayerEndOfTurn());
        }
    }

    IEnumerator PlayerAttack2()
    {
        #region old
        // attack stuff
        /*
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
        */
        #endregion old
        GameObject timingGO = Instantiate(attack[1], actions.transform.parent);
        TimingController timingCon = timingGO.GetComponent<TimingController>();
        timingCon.startTiming(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || !timingCon.isActivated);
        playerUnit.animationStart("attacking");
        HitTiming hitStatus = timingCon.Clicked();
        switch (hitStatus)
        {
            case (HitTiming.Miss):
                dialogue.text = "You missed your attack!";
                break;

            case (HitTiming.Hit):
                dialogue.text = "You hit your attack!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.damage) + " damage!";
                yield return new WaitForSeconds(1);
                enemyUnit.addStatus(timingCon.regularHitStatus, timingCon.hitStatusStacks);
                yield return new WaitForSeconds(1);
                break;

            case (HitTiming.Critical):
                dialogue.text = "You critically hit your attack!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.CritDamage) + " damage!";
                yield return new WaitForSeconds(1);
                enemyUnit.addStatus(timingCon.criticalHitStatus, timingCon.criticalStatusStacks);
                break;
        }
        Destroy(timingGO);

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
        Debug.Log("PlayerTurn ended");
        if (!playerUnit.bleedImmune && playerUnit.bleed > 0)
        {
            dialogue.text = "You've taken " + playerUnit.takeDamage(playerUnit.bleed) + " bleed damage";
            playerUnit.addStatus(statusEffects.Bleed, -1);
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
            dialogue.text = "Enemy has taken " + playerUnit.takeDamage(enemyUnit.poison) + " poison damage!";
            if (enemyUnit.poison == 1)
            {
                enemyUnit.addStatus(statusEffects.Poison, -1);
            }
            else
            {
                enemyUnit.addStatus(statusEffects.Poison, (int)Mathf.Floor(-enemyUnit.poison / 2f));
            }
        }
        if (CheckStatuses(enemyUnit, statusEffects.Stunned))
        {
            if (enemyUnit.stunned >= 1)
            {
                dialogue.text = "Enemy has been stunned";
                enemyUnit.addStatus(statusEffects.Stunned, -1);
                yield return new WaitForSeconds(1);
                StartCoroutine(EnemyEndOfTurn());
                yield break;
            }
        }
        yield return new WaitForSeconds(1f);
        if (CheckHealth())
        {
            yield break;
        }
        int randomNumber = Random.Range(0, enemyTimings.Count - 1);
        Debug.Log("RandomIndex = " + randomNumber);
        StartCoroutine(EnemyAttack(randomNumber));
    }

    IEnumerator EnemyAttack(int attackIndex)
    {
        #region old
        /*
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
            dialogue.text = playerUnit.unitName + " has taken " + playerUnit.takeDamage(rolledNumber + enemyUnit.damageBonus) + " damage!";
            playerHUD.SetHP(playerUnit.curHP);
            yield return new WaitForSeconds(2);
        }
        rolledNumber = 0;
        */
        #endregion old
        GameObject timingGO = Instantiate(enemyTimings[attackIndex].timing, actions.transform.parent);
        TimingController timingCon = timingGO.GetComponent<TimingController>();
        timingCon.startTiming(true);
        yield return new WaitUntil(() => (Input.GetMouseButtonDown(0) || !timingCon.isActivated));
        enemyUnit.animationStart("attacking");
        HitTiming hitStatus = timingCon.Clicked();
        switch (hitStatus)
        {
            case (HitTiming.Miss):
                dialogue.text = "The enemy critically hit!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "You have taken " + playerUnit.takeDamage(timingCon.CritDamage) + " damage!";
                yield return new WaitForSeconds(1);
                playerUnit.addStatus(timingCon.criticalHitStatus, timingCon.criticalStatusStacks);
                break;

            case (HitTiming.Hit):
                dialogue.text = "The attack scraped you!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "You have taken " + playerUnit.takeDamage(timingCon.damage) + " damage!";
                yield return new WaitForSeconds(1);
                playerUnit.addStatus(timingCon.regularHitStatus, timingCon.hitStatusStacks);
                break;

            case (HitTiming.Critical):
                dialogue.text = "You blocked most of the enemy's attack!";
                yield return new WaitForSeconds(1);
                timingGO.SetActive(false);
                dialogue.text = "You have taken " + playerUnit.takeDamage(timingCon.damage / 2) + " damage!";
                break;
        }
        Destroy(timingGO);

        yield return new WaitForSeconds(1);

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
            dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(enemyUnit.bleed) + " bleed damage!";
            enemyUnit.addStatus(statusEffects.Bleed, -1);
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
        PlayerPrefs.SetInt("playerHealth", playerUnit.curHP);
        
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
                if(unitToCheck.stunned >= 1)
                {
                    return true;
                }
                return false;

            case statusEffects.Bleed:
                if(!unitToCheck.bleedImmune && unitToCheck.bleed >= 1)
                {
                    return true;
                }
                return false;

            case statusEffects.Poison:
                if (!unitToCheck.poisonImmune && unitToCheck.poison >= 1)
                {
                    return true;
                }
                return false;
        }
    }

    public void ClearStatus(Unit unit)
    {
        unit.addStatus(statusEffects.Stunned, -unit.stunned);
        unit.addStatus(statusEffects.Poison, -unit.poison);
        unit.addStatus(statusEffects.Bleed, -unit.bleed);
    }

    public bool CheckHealth()
    {
        if (playerUnit.curHP <= 0)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
            return true;
        }
        else if (enemyUnit.curHP <= 0)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
            return true;
        }
        return false;
    }

    public void ReceiveRoll(int roll)
    {
        Debug.Log("Received Result");
    }

    public void ClearEnemies()
    {
        Destroy(enemyUnit.gameObject);
    }
}
