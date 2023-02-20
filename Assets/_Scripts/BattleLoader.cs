using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    private BattleSystem BS;

    private GameObject winEvent;

    private static BattleLoader battleLoaderInstance;

    private void Awake()
    {
        if (battleLoaderInstance == null)
        {
            battleLoaderInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBattle(GameObject enemyPrefab, GameObject WinEvent)
    {
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("PROTOTYPE_TURN_BASED_COMBAT", LoadSceneMode.Additive);
        }
        Debug.Log("Loading New Scene");
        StartCoroutine(SwitchScenes(enemyPrefab, WinEvent));
    }

    IEnumerator SwitchScenes(GameObject enemyPrefab, GameObject WinEvent)
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByName("PROTOTYPE_TURN_BASED_COMBAT").isLoaded);
        Debug.Log("Scene Loaded");
        BS = FindObjectOfType<BattleSystem>();
        BS.enemyPrefab = enemyPrefab;
        winEvent = WinEvent;
        BS.startSetup();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PROTOTYPE_TURN_BASED_COMBAT"));
        GetComponent<ModeSwap>().ChangeToCombat();
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
