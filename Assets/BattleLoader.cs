using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    private BattleSystem BS;

    private GameObject winEvent;

    public void StartBattle(GameObject enemyPrefab, GameObject WinEvent)
    {
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("PROTOTYPE_TURN_BASED_COMBAT", LoadSceneMode.Additive);
        }
        BS = FindObjectOfType<BattleSystem>();
        BS.enemyPrefab = enemyPrefab;
        winEvent = WinEvent;
    }

    public void EndBattle()
    {
        if (PlayerPrefs.GetString("BattleResult") == "Won")
        {
            
        }
        else
        {
            SceneManager.LoadScene("MAIN_MENU");
        }
    }
}
