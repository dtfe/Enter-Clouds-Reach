using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EnterCloudsReach.EventSystem;

namespace EnterCloudsReach.Combat{

    public class BattleLoader : MonoBehaviour
    {
        [SerializeField]private BattleSystem BS;

        private GameObject winEvent;
        private EventClass winEventNew;

        public Canvas canvas;

        private static BattleLoader battleLoaderInstance;

        private void Awake()
        {
            PlayerPrefs.DeleteAll();
            if (battleLoaderInstance == null)
            {
                battleLoaderInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
         void Update()
        {
            if(canvas == null)
            {
                GameObject[] goArr = GameObject.FindGameObjectsWithTag("Exploration");
                foreach(GameObject go in goArr)
                {
                    if(go.TryGetComponent<Canvas>(out canvas))
                    {
                        canvas = go.GetComponent<Canvas>();
                    }
                }
            }
        }

        public void StartBattle(GameObject enemyPrefab, GameObject WinEvent, BattleType type)
        {
            Debug.Log("Loading Combat");
            StartCoroutine(SwitchScenes(enemyPrefab, WinEvent, type));
        }
        public void StartBattleNew(GameObject enemyPrefab, EventClass WinEvent, BattleType type)
        {
            Debug.Log("Loading Combat");
            StartCoroutine(SwitchScenesNew(enemyPrefab, WinEvent, type));
        }

        IEnumerator SwitchScenes(GameObject enemyPrefab, GameObject WinEvent, BattleType type)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Scene Loaded");
            GetComponent<ModeSwap>().ChangeToCombat();
            BS = FindObjectOfType<BattleSystem>();
            BS.enemyPrefab = enemyPrefab;
            winEvent = WinEvent;
            BS.startSetup(type);
        }
        IEnumerator SwitchScenesNew(GameObject enemyPrefab, EventClass WinEvent, BattleType type)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Scene Loaded");
            GetComponent<ModeSwap>().ChangeToCombat();
            BS = FindObjectOfType<BattleSystem>();
            BS.enemyPrefab = enemyPrefab;
            winEventNew = WinEvent;
            BS.startSetup(type);
        }

        public void EndBattle()
        {
            if (PlayerPrefs.GetString("BattleResult") == "Won")
            {
                BS.ClearUnits();
                GetComponent<ModeSwap>().ChangeToExploration();
                EventClass eventClass = FindObjectOfType<EventClass>();
                if(eventClass != null){
                eventClass.EndEvent(winEventNew);}
                else {
                Instantiate(winEvent, canvas.transform);}
            }
            else
            {
                SceneManager.LoadScene("MAIN_MENU_NEW");
            }
        }
    }
}