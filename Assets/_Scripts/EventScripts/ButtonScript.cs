using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnterCloudsReach.Combat;

public class ButtonScript : MonoBehaviour
{
    [Header("Button Properties")]
    private Button self;
    private Button[] buttons;

    [Header("Battle Properties")]
    public GameObject enemyPrefab;
    public GameObject winEvent;

    public BattleType type;

    // Start is called before the first frame update
    void Start()
    {
        buttons = FindObjectOfType<UiAnimatorFinal>().GetComponentsInChildren<Button>();
        self = GetComponent<Button>();
        foreach(Button i in buttons)
        {
            self.onClick.AddListener(() => { DeactivateButton(i); Debug.Log("deactivated"); });
        }
    }

    public void EngageBattle()
    {
        FindObjectOfType<BattleLoader>().StartBattle(enemyPrefab, winEvent, type);
    }

    public void movePlayerToPoint(string ID)
    {
        FindObjectOfType<NavmeshPointFinderScript>().findPoint(ID);
    }


    public void ActivateButton()
    {
        self.interactable = true;
    }
    public void DeactivateButton(Button btn)
    {
        btn.interactable = false;
    }
}
