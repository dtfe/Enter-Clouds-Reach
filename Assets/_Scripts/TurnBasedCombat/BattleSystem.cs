using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EnterCloudsReach.Player;
using EnterCloudsReach.Inventory;
public enum BattleState 
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}


public enum BattleType
{
    Regular,
    GoblinBoss
}
namespace EnterCloudsReach.Combat
{

    public class BattleSystem : MonoBehaviour, IReceiveResult
    {
        [Header("Properties")]
        public TMP_Text dialogue;
        public GameObject actions;
        private CombatMinigameManager MGManager;
        [SerializeField] private GameObject cam;
        [SerializeField] private Transform[] camPos;

        [Header("Arena Specific")]
        [SerializeField] private GameObject[] ArenaEnvironment;
        [SerializeField] private Transform[] enemySpawnPoints;
        [SerializeField] private Transform[] playerSpawnPoints;

        [Header("Attack Prefabs")]
        [SerializeField]public GameObject[] attack;

        private GameObject[] defaultAttack;

        [Header("Player Properties")]
        public GameObject playerPrefab;
        public Transform playerSpawn;

        [Header("Enemy Properties")]
        public GameObject enemyPrefab;
        public Transform enemySpawn;
        public List<DefendTimings> enemyTimings = new List<DefendTimings>();

        private BattleHUD playerHUD;
        private BattleHUD enemyHUD;
        private int enemyCurAttackIndex = 0;

        [Header("Game Information")]
        public BattleState state;
        [SerializeField] private bool startBattleOnStart = false;
        [SerializeField] private BattleType typeToStart;

        private RollManager rm;

        Unit playerUnit;
        Unit enemyUnit;
        CombatSFX enemyCSFX;
        CombatSFX playerCSFX;
        PlayerStats playerStats;
        EquipmentSlotManager eqM;
        Button[] attackButtons;



        // Start is called before the first frame update
        void Start()
        {
            MGManager = GetComponent<CombatMinigameManager>();
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
            eqM = FindObjectOfType<EquipmentSlotManager>();
            rm = FindObjectOfType<RollManager>();
            attackButtons = actions.GetComponentsInChildren<Button>();
            if (startBattleOnStart)
            {
                StartCoroutine(SetupBattle(typeToStart));
            }
        }

        #region battleSetup
        public void startSetup(BattleType type)
        {
            StartCoroutine(SetupBattle(type));
        }

        IEnumerator SetupBattle(BattleType type)
        {
            int battletypeIndex = 0;
            switch (type)
            {
                default:
                    battletypeIndex = 0;
                    break;

                case BattleType.GoblinBoss:
                    battletypeIndex = 1;
                    break;
            }
            cam.transform.position = camPos[battletypeIndex].position;
            cam.transform.rotation = camPos[battletypeIndex].rotation;
            for (int i = 0; i < ArenaEnvironment.Length; i++)
            {
                ArenaEnvironment[i].SetActive(false);
                if (i == battletypeIndex)
                {
                    ArenaEnvironment[i].SetActive(true);
                }
            }
            playerSpawn = playerSpawnPoints[battletypeIndex];
            enemySpawn = enemySpawnPoints[battletypeIndex];


            state = BattleState.START;
            if (playerUnit == null)
            {
                GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
                playerCSFX = playerGO.GetComponent<CombatSFX>();
                playerUnit = playerGO.GetComponent<Unit>();
                playerHUD = playerGO.transform.Find("HUD").GetComponent<BattleHUD>();
                //This wont work with saving
                //PlayerPrefs.SetInt("playerHealth", playerUnit.maxHP);
            }
            //playerUnit.curHP = PlayerPrefs.GetInt("playerHealth");
            if(playerStats)
            {
                playerUnit.curHP = playerStats.health;
                playerUnit.maxHP = playerStats.maxHealth;
            }
            else
            {
                playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
                playerUnit.curHP = playerStats.health;
                playerUnit.maxHP = playerStats.maxHealth;
            }
            
            //SetAttacks();


            GameObject enemyGO = Instantiate(enemyPrefab, enemySpawn);
            enemyUnit = enemyGO.GetComponent<Unit>();
            enemyCSFX = enemyGO.GetComponent<CombatSFX>();
            enemyHUD = enemyGO.transform.Find("HUD").GetComponent<BattleHUD>();
            enemyCurAttackIndex = 0;

            enemyHUD.SetHUD(enemyUnit);

            yield return new WaitForSeconds(0f);
            playerHUD.SetHUD(playerUnit);
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
                    enemyCSFX?.PlayHitAudio(hitStatus);
                    yield return new WaitForSeconds(1);
                    break;

                case (HitTiming.Hit):
                    dialogue.text = "Hit!";
                    yield return new WaitForSeconds(1);
                    timingGO.SetActive(false);
                    dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(timingCon.damage) + " damage!";
                    enemyCSFX?.PlayHitAudio(hitStatus);
                    yield return new WaitForSeconds(1);
                    enemyUnit.addStatus(timingCon.regularHitStatus, timingCon.hitStatusStacks);
                    yield return new WaitForSeconds(1);
                    break;

                case (HitTiming.Critical):
                    dialogue.text = "Critical Hit!";
                    yield return new WaitForSeconds(1);
                    timingGO.SetActive(false);
                    enemyCSFX?.PlayHitAudio(hitStatus);
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
                dialogue.text = "Enemy has taken " + enemyUnit.takeDamage(enemyUnit.poison) + " poison damage!";
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
            #region oldCombat
            //int randomNumber = Random.Range(0, enemyTimings.Count - 1);
            //Debug.Log("RandomIndex = " + randomNumber);
            //StartCoroutine(EnemyAttack(randomNumber));
            #endregion oldCombat
            StartCoroutine(EnemySequenceAttacks(enemyCurAttackIndex));
            enemyCurAttackIndex++;
            if (enemyCurAttackIndex + 1 > enemyUnit.sequences.Length)
            {
                enemyCurAttackIndex = 0;
                Debug.Log("Restarting sequence queue");
            }
        }

        IEnumerator EnemySequenceAttacks(int attackIndex)
        {
            MGManager.SetSequence(enemyUnit.sequences[attackIndex]);
            MGManager.StartMinigame();
            yield return new WaitUntil(() => MGManager.isPlaying == false);

            enemyUnit.animationStart("attacking");
            dialogue.text = "Player has taken a total of " + (MGManager.misses * enemyUnit.damage) + " damage!";

            yield return new WaitForSeconds(2);

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
            // PlayerPrefs.SetInt("playerHealth", playerUnit.curHP);
            playerStats.health = playerUnit.curHP;
            yield return new WaitForSeconds(2f);
            if (FindObjectOfType<BattleLoader>())
            {
                FindObjectOfType<BattleLoader>().EndBattle();
            }
            else if (FindObjectOfType<CombatSceneController>())
            {
                playerStats.health = playerStats.maxHealth;
                FindObjectOfType<CombatSceneController>().RestartScene();
            }

        }

        private bool CheckStatuses(Unit unitToCheck, statusEffects status)
        {
            switch (status)
            {
                default:
                    Debug.Log("Status was not found, returning false");
                    return false;


                case statusEffects.Stunned:
                    if (unitToCheck.stunned >= 1)
                    {
                        return true;
                    }
                    return false;

                case statusEffects.Bleed:
                    if (!unitToCheck.bleedImmune && unitToCheck.bleed >= 1)
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

        public void ClearUnits()
        {
            Destroy(enemyUnit.gameObject);
            Destroy(playerUnit.gameObject);
        }

        public void DealDamageToPlayer(int numberOfMisses)
        {
            enemyUnit.animationStart("attacking");
            playerUnit.takeDamage(numberOfMisses * enemyUnit.damage);
        }

        public void ApplyStatusToPlayer(statusEffects statusToApply)
        {
            playerUnit.addStatus(statusToApply, 1);
        }

        /*
        private void SetAttacks()
        {
            if (eqM.eqWeapons.Any())
            {
                switch (eqM.eqWeapons.Count)
                {
                    case 1:
                        attack = eqM.eqWeapons[0].attacks;
                        break;
                    case 2:
                        Item fWeapon = eqM.eqWeapons[0];
                        Item sWeapon = eqM.eqWeapons[1];
                        //make a temp array
                        GameObject[] go;
                        //if weapons are the same don't bother with the second weapon Don't know if we are going to use ids
                        if (fWeapon.id == sWeapon.id && fWeapon.itemName == sWeapon.itemName)
                        {
                            go = fWeapon.attacks;
                        }
                        else
                        {
                            go = fWeapon.attacks.Concat(sWeapon.attacks).ToArray();
                        }
                        //Now time to remove duplicate attacks, which SHOULDN'T happen with the same item but it might happen;
                        attack = go.Distinct().ToArray();
                        break;
                }
            }
            else
            {
                attack = defaultAttack;
            }
            //remove all nulls
            attack = attack.Where(c => c != null).ToArray();
            //if all attacks where null
            if (attack.Length == 0)
            {
                attack = defaultAttack;
            }
            TMP_Text[] attackText = new TMP_Text[attackButtons.Length];
            for (int i = 0; i < attackButtons.Length; i++)
            {

                attackText[i] = attackButtons[i].GetComponentInChildren<TMP_Text>();
                attackButtons[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < attack.Length; i++)
            {
                attackText[i].SetText(attack[i].name);
                attackButtons[i].gameObject.SetActive(true);
            }
        }*/
    }
}