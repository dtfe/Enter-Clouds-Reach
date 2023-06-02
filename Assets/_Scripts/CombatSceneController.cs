using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnterCloudsReach.Combat
{

    public class CombatSceneController : MonoBehaviour
    {
        [SerializeField] private BattleSystem bs;
        [SerializeField] private ModeSwap modeSwap;

        public void LoadEnemyBattle(GameObject enemyToSpawn)
        {
            modeSwap.ChangeToCombat();
            bs.enemyPrefab = enemyToSpawn;
            if (enemyToSpawn.name == "KingGob")
            {
                bs.startSetup(BattleType.GoblinBoss);
            }
            else
            {
                bs.startSetup(BattleType.Regular);
            }
        }

        public void RestartScene()
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
