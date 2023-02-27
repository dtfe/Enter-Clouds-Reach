using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleLoader : MonoBehaviour
{
    private BattleSystem BS;

    private GameObject winEvent;

    public Canvas canvas;

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
        Debug.Log("Loading Combat");
        StartCoroutine(SwitchScenes(enemyPrefab, WinEvent));
    }

    IEnumerator SwitchScenes(GameObject enemyPrefab, GameObject WinEvent)
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Scene Loaded");
        BS = FindObjectOfType<BattleSystem>();
        BS.enemyPrefab = enemyPrefab;
        winEvent = WinEvent;
        BS.startSetup();
        GetComponent<ModeSwap>().ChangeToCombat();
    }

    public void EndBattle()
    {
        if (PlayerPrefs.GetString("BattleResult") == "Won")
        {
            GetComponent<ModeSwap>().ChangeToExploration();
            Instantiate(winEvent, canvas.transform);
        }
        else
        {
            SceneManager.LoadScene("MAIN_MENU");
        }
    }
}
